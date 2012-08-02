using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace auction_2
{
    public class Auction
    {

        private readonly List<Bid> _bids;
        private readonly List<Lot> _lots;
        private readonly List<Sale> _sales;
        private readonly List<Series> _series;
        private readonly List<Category> _categories;
        private readonly List<Seller> _sellers;
        private readonly List<Buyer> _buyers; 
        public ReadOnlyCollection<Bid> Bids { get{return new ReadOnlyCollection<Bid>(_bids);}}
        public ReadOnlyCollection<Lot> Lots { get {return new ReadOnlyCollection<Lot>(_lots);}} 
        public ReadOnlyCollection<Sale> Sales { get { return new ReadOnlyCollection<Sale>(_sales); } }
        public ReadOnlyCollection<Series> Series { get { return new ReadOnlyCollection<Series>(_series); } }
        public ReadOnlyCollection<Category> Categories {get {return new ReadOnlyCollection<Category>(_categories); } }
        public ReadOnlyCollection<Seller> Sellers {get{return new ReadOnlyCollection<Seller>(_sellers);}} 
        public ReadOnlyCollection<Buyer> Buyers {get {return new ReadOnlyCollection<Buyer>(_buyers);}}
        public AuctionSettings Settings { get; private set; }
        



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

        }

        public void AddBuyer(Buyer buyer)
        {
            if (_buyers.Count(b => b.Login == buyer.Login) == 0)
            {
                _buyers.Add(buyer);
            }
        }
        public void AddSeller(Seller seller)
        {
            if (_sellers.Count(s => s.Login == seller.Login) == 0)
                _sellers.Add(seller);
        }
        public void AddCategory(Category category)
        {
            if (_categories.Count(c => c.Name == category.Name) == 0)
            {
                _categories.Add(category);
            }
        }
        public void AddSeries(Series series)
        {
            if (_series.Count(s => s.Name == series.Name) == 0)
            {
                _series.Add(series);
            }
        }
        public void AddSale(Sale sale)
        {
            if (IsCorrectSale(sale))
            {
                var lastOrDefault = _sales.LastOrDefault();
                if (lastOrDefault != null) sale.Number = lastOrDefault.Number + 1;

                if (sale.Duration < Settings.MinSaleDuration)
                {
                    sale.Duration = Settings.MinSaleDuration;
                }


                AddLot(sale.Lot);
                _sales.Add(sale);
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
            if (IsCorrectBid(bid))
            {
                _bids.Add(bid);
                bid.Sale.LastBid = bid;
            }
        }

        // возвращает сумму успешных покупок
        private double GetBuyerIndex(Buyer buyer)
        {
            return GetSuccessfulSales(buyer).Sum(s => s.CurrentPrice);
        }
        private bool IsCorrectBid(Bid bid)
        {
            var s = bid.Sale;
            return s.IsActive &&
                   (bid.Value - s.Increment >= s.CurrentPrice) &&
                   s.Category.Restriction <= GetBuyerIndex(bid.Bidder) &&
                   (s.LastBid == null || s.LastBid.Bidder != bid.Bidder);

        }

        //ex ?
        private bool IsCorrectSale(Sale sale)
        {
            return !_sales.Contains(sale) && (_sellers.Contains(sale.Seller) && _series.Contains(sale.Series)) &&
                   sale.Lot != null;
        }
    }

}
