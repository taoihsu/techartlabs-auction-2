using System.Collections.Generic;
using Auction.Users;

namespace Auction.Interfaces
{
    public interface IActiveBuyersBehavior
    {
        IEnumerable<Buyer> GetActiveBuyers(IEnumerable<Sale> sales, IEnumerable<Bid> bids, double percentage);
    }
}
