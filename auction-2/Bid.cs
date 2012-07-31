using System;

namespace auction_2
{
    public class Bid
    {
        public Sale Sale { get; private set; }
        public double Value { get; private set; }
        public Buyer Bidder { get; private set; }
        public DateTime Time { get; private set; }

        public Bid(Sale sale, double value, Buyer bidder)
        {
            Sale = sale;
            Value = value;
            Bidder = bidder;
            Time = DateTime.Now;
        }
    }
}
