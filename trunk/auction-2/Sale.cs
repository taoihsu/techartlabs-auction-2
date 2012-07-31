using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace auction_2
{
    public class Sale
    {
        public string Name { get; private set; }
        public int Number { get; set; }
        public Lot Lot { get; private set; }
        public Category Category { get; private set; }
        private readonly List<Bid> _bids;
        public ReadOnlyCollection<Bid> Bids { get { return new ReadOnlyCollection<Bid>(_bids);} } 
        public Seller Seller { get; private set; }
        public Buyer Buyer { get { return !IsSaled ? null : LastBidder; } }
        public Buyer LastBidder
        {
            get
            {
                var lastOrDefault = Bids.LastOrDefault();
                return lastOrDefault != null ? lastOrDefault.Bidder : null;
            }
        }

        //public bool CanBuyOut { get; private set; }
        //public double BuyOutPrice { get; private set; }

        public double StartPrice { get; private set; }
        public double Increment { get; private set; }
        public double CurrentPrice
        {
            get { return ((Bids.Count == 0) ? StartPrice:  Bids.Last().Value); }
        }

        public DateTime StartTime { get; private set; }
        public TimeSpan Duration { get; set; }
        public DateTime FinishTime { get { return StartTime + Duration; } }
        public TimeSpan TimeElapsed { get { return DateTime.Now - StartTime; } }
        public bool IsTimeExpired { get { return DateTime.Now >= FinishTime; } }

        public bool IsActive
        {
            get { return (!IsTimeExpired); }
        }
        public bool IsSaled { get { return (Bids.Count > 0) && (IsTimeExpired); } }

        public Sale(string name, Lot lot, Seller seller, double startPrice, 
            double increment, TimeSpan duration, Category category)
        {
            Name = name;
            Number = 0;
            Lot = lot;
            StartTime = DateTime.Now;
            _bids = new List<Bid>();
            StartPrice = startPrice;
            Increment = increment;
            Seller = seller;
          
            if (duration < TimeSpan.FromMinutes(1))
            {
                duration = TimeSpan.FromSeconds(1); //исправить! FromMinutes(1)
            }
            Duration = duration;
            Category = category;
        }

        public void RegisterBid(Bid bid)
        {
            if (CorrectBid(bid))
            {
                _bids.Add(bid);
            }
        }

        private bool CorrectBid(Bid bid)
        {
            return (bid.Value - CurrentPrice >= Increment) && (DateTime.Now < FinishTime) && (LastBidder != bid.Bidder);
        }
    }
}
