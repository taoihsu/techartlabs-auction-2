using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Auction.ActiveBuyersBehavior;
using Auction.Events;
using Auction.Interfaces;
using Auction.Users;

namespace Auction
{
    public class Auction
    {
        #region private fields

        private readonly List<Bid> _bids;
        private readonly List<Lot> _lots;
        private readonly List<Sale> _sales;
        private readonly List<Series> _series;
        private readonly List<Category> _categories;
        private readonly List<Seller> _sellers;
        private readonly List<Buyer> _buyers;

        #endregion

        public Auction()
        {
            _bids = new List<Bid>();
            _lots = new List<Lot>();
            _sales = new List<Sale>();
            _series = new List<Series>();
            _categories = new List<Category>();
            _sellers = new List<Seller>();
            _buyers = new List<Buyer>();

            Settings = new AuctionSettings();
            ActiveBuyersBehavior = new SummarySalesBehavior();

        }

        public AuctionSettings Settings { get; private set; }

        public IActiveBuyersBehavior ActiveBuyersBehavior { get; set; }

        #region Events

        public event EventHandler<ActionEventArgs<Sale>> SaleStarted;

        public event EventHandler<ActionEventArgs<Bid>> BidMaked;

        public event EventHandler<ActionEventArgs<Sale>> SaleFinished;

        #endregion

        public ReadOnlyCollection<Bid> Bids
        {
            get { return new ReadOnlyCollection<Bid>(_bids); }
        }

        public ReadOnlyCollection<Lot> Lots
        {
            get { return new ReadOnlyCollection<Lot>(_lots); }
        }

        public ReadOnlyCollection<Sale> Sales
        {
            get { return new ReadOnlyCollection<Sale>(_sales); }
        }

        public ReadOnlyCollection<Series> Series
        {
            get { return new ReadOnlyCollection<Series>(_series); }
        }

        public ReadOnlyCollection<Category> Categories
        {
            get { return new ReadOnlyCollection<Category>(_categories); }
        }

        public ReadOnlyCollection<Seller> Sellers
        {
            get { return new ReadOnlyCollection<Seller>(_sellers); }
        }

        public ReadOnlyCollection<Buyer> Buyers
        {
            get { return new ReadOnlyCollection<Buyer>(_buyers); }
        }

        public void AddBuyer(Buyer buyer)
        {
            if (!_buyers.Any(b => b.Login == buyer.Login))
            {
                _buyers.Add(buyer);
            }
        }

        public void AddSeller(Seller seller)
        {
            if (!_sellers.Any(s => s.Login == seller.Login))
            {
                _sellers.Add(seller);
            }
        }

        public void AddCategory(Category category)
        {
            if (!_categories.Any(c => c.Name == category.Name))
            {
                _categories.Add(category);
            }
        }

        public void AddSeries(Series series)
        {
            if (!_series.Any(s => s.Name == series.Name))
            {
                _series.Add(series);
            }
        }

        public void AddSale(Sale sale)
        {
            if (IsValidSale(sale))
            {
                var lastOrDefault = _sales.LastOrDefault();
                if (lastOrDefault != null)
                {
                    sale.Number = lastOrDefault.Number + 1;
                }

                if (sale.Duration < Settings.MinSaleDuration)
                {
                    sale.Duration = Settings.MinSaleDuration;
                }

                AddLot(sale.Lot);
                _sales.Add(sale);
                OnSaleCreated(sale);
                sale.SaleFinished += SaleFinish;
                sale.Start();
            }
        }

        public void AddLot(Lot lot)
        {
            if (!_lots.Contains(lot))
            {
                _lots.Add(lot);
            }
        }

        public IEnumerable<Sale> GetSuccessfulSales()
        {
            return _sales.Where(s => s.IsSaled);
        }

        public IEnumerable<Sale> GetSuccessfulSales(Buyer buyer)
        {
            return _sales.Where(s => s.IsSaled && s.Buyer == buyer);
        }

        public IEnumerable<Sale> GetSuccessfulSales(Seller seller)
        {
            return _sales.Where(s => s.IsSaled && s.Seller == seller);
        }

        public void MakeBid(Bid bid)
        {
            if (IsValidBid(bid))
            {
                _bids.Add(bid);
                bid.Sale.LastBid = bid;
                OnBidMaked(bid);
            }
        }

        public IEnumerable<Buyer> GetActiveBuyers(double percentage)
        {
            return ActiveBuyersBehavior.GetActiveBuyers(_sales, _bids, percentage);
        }

        private double GetBuyerIndex(Buyer buyer)
        {
            return GetSuccessfulSales(buyer).Sum(s => s.CurrentPrice);
        }

        private bool IsValidBid(Bid bid)
        {
            var s = bid.Sale;
            return s.IsActive &&
                   (bid.Value - s.Increment >= s.CurrentPrice) &&
                   s.Category.Restriction <= GetBuyerIndex(bid.Bidder) &&
                   (s.LastBid == null || s.LastBid.Bidder != bid.Bidder);
        }

        private bool IsValidSale(Sale sale)
        {
            return !_sales.Contains(sale) && (_sellers.Contains(sale.Seller) && _series.Contains(sale.Series)) &&
                   sale.Lot != null;
        }

        protected void SaleFinish(object sender, ActionEventArgs<Sale> args)
        {
            args.EventInfo.SaleFinished -= SaleFinish;
            OnSaleFinished(args.EventInfo);
        }
        
        protected virtual void OnSaleFinished(Sale sale)
        {
            EventHandler<ActionEventArgs<Sale>> handler = SaleFinished;
            if (handler != null) handler(this, new ActionEventArgs<Sale>(sale));
        }

        protected virtual void OnBidMaked(Bid newBid)
        {
            EventHandler<ActionEventArgs<Bid>> handler = BidMaked;
            if (handler != null) handler(this, new ActionEventArgs<Bid>(newBid));
        }

        protected virtual void OnSaleCreated(Sale newSale)
        {
            EventHandler<ActionEventArgs<Sale>> handler = SaleStarted;
            if (handler != null) handler(this, new ActionEventArgs<Sale>(newSale));
        }
    }

}