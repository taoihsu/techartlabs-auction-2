using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace auction_2
{
    public class Sale
    {
        //
        public string Name { get; private set; }
        public int Number { get; set; }
        public Lot Lot { get; private set; }
        public Category Category { get; private set; }
        public Series Series { get; private set; }
        public Bid LastBid { get; private set; } //private?
        public Seller Seller { get; private set; }
        public Buyer Buyer { get { return !IsSaled ? null : LastBid.Bidder; } }

        public double StartPrice { get; private set; }
        public double Increment { get; private set; } // get from config?
        public double CurrentPrice 
        {
            get { return ((LastBid==null) ? StartPrice:  LastBid.Value); }
        }

        public DateTime StartTime { get; private set; }
        public TimeSpan Duration { get; set; }
        public DateTime FinishTime { get { return StartTime + Duration; } }

        public bool IsStarted { get { return DateTime.Now >= StartTime; } }
        public bool IsFinished { get { return DateTime.Now >= FinishTime; } }
        public bool IsActive { get { return IsStarted && !IsFinished; } }
        public bool IsSaled { get { return (LastBid!=null) && (IsFinished); } }

        public Sale(string name, Lot lot, Series series, Seller seller, double startPrice, 
            double increment, TimeSpan duration, Category category)
        {
            Name = name;
            Number = 0;
            Lot = lot;
            Series = series;
            StartTime = DateTime.Now;
            StartPrice = startPrice;
            Increment = increment;
            Seller = seller;
          
            if (duration < TimeSpan.FromMinutes(1))
            {
                duration = TimeSpan.FromSeconds(1); //исправить! проверка на уровне Аукциона
            }
            Duration = duration;
            Category = category;
        }

        // перенести в обязанности аукциона
        private bool CorrectBid(Bid bid)
        {
            return (bid.Value - CurrentPrice >= Increment) && (DateTime.Now < FinishTime) && (LastBidder != bid.Bidder);
        }
    }
}
