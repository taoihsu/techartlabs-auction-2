using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        public void MakeBid()
        {
            
        }
    }
}
