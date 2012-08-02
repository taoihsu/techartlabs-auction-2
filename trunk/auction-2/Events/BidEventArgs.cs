using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace auction_2.Events
{
    public class BidEventArgs: EventArgs
    {
        public Bid Bid { get; private set; }

        public BidEventArgs(Bid newBid)
        {
            Bid = newBid;
        }

    }
}
