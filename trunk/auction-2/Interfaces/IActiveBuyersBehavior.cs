using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace auction_2.Interfaces
{
    public interface IActiveBuyersBehavior
    {
        IEnumerable<Buyer> GetActiveBuyers(IEnumerable<Sale> sales, IEnumerable<Bid> bids, double percentage);
    }
}
