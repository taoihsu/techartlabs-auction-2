using System.Collections.Generic;
using System.Linq;
using Auction.Interfaces;
using Auction.Users;

namespace Auction.ActiveBuyersBehavior
{
    public class SummarySalesBehavior : IActiveBuyersBehavior
    {
        public IEnumerable<Buyer> GetActiveBuyers(IEnumerable<Sale> sales, IEnumerable<Bid> bids, double percentage)
        {
            if (percentage > 100 || percentage <= 0)
            {
                percentage = 100;
            }
            var buyerSumPrice = new Dictionary<Buyer, double>();
            foreach (var s in sales.Where(s => s.IsSaled))
            {
                if (buyerSumPrice.ContainsKey(s.Buyer))
                {
                    buyerSumPrice[s.Buyer] += s.LastBid.Value;
                }
                else
                {
                    buyerSumPrice.Add(s.Buyer, s.LastBid.Value);
                }
            }

            var sorted = buyerSumPrice.OrderByDescending(p => p.Value);
            var count = sorted.Count();
            var requiredCount = count*percentage/100;
            return sorted.Take((int) requiredCount).Select(p => p.Key);

        }
    }
}
