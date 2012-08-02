﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using auction_2.Events;

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
        private Bid _lastBid;
        public Bid LastBid
        {
            get { return _lastBid; }
            set { 
                _lastBid = value;
                OnBidMaked(value);
            }
        }
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
        public bool IsFinished { get { return DateTime.Now > FinishTime; } }
        public bool IsActive { get { return IsStarted && !IsFinished; } }
        public bool IsSaled { get { return (LastBid!=null) && (IsFinished); } }

        public event EventHandler<BidEventArgs> BidMaked;
        public event EventHandler<EventArgs> SaleFinished;

        public Sale(string name, Lot lot, Series series, Seller seller, double startPrice, 
            double increment, TimeSpan duration, Category category)
        {
            Name = name;
            Number = 0;
            Lot = lot;
            Series = series;
            StartPrice = startPrice;
            Increment = increment;
            Seller = seller;
            Duration = duration;
            Category = category;
        }

        public void Start()
        {
            StartTime = DateTime.Now;
            var waitForfinishThread = new Thread(WaitForFinish);
            waitForfinishThread.Start();          
        }

        private void WaitForFinish()
        {
            //вычислять точное значеие?
            Thread.Sleep(Duration);
            OnSaleFinished(LastBid);

        }

        protected virtual void OnBidMaked(Bid newBid)
        {
            EventHandler<BidEventArgs> local = BidMaked;
            if (local != null)
            {
                local(this,new BidEventArgs(newBid));
            }
        }
        protected virtual void OnSaleFinished(Bid lastBid)
        {
            EventHandler<EventArgs> local = SaleFinished;
            if (local != null)
            {
                local(this, new EventArgs());
            }
        }
    }
}
