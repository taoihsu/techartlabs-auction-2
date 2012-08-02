using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace auction_2
{
    public class Series
    {
        public string Name { get; private set; }
        //private readonly List<Sale> _sales;
        //public ReadOnlyCollection<Sale> Sales { get { return new ReadOnlyCollection<Sale>(_sales); } }
        //public ReadOnlyCollection<Bid> Bids
        //{
        //    get
        //    {
        //        var allBids = new List<Bid>();
        //        foreach (var sale in _sales)
        //        {
        //            allBids.AddRange(sale.Bids);
        //        }
        //        return new ReadOnlyCollection<Bid>(allBids);
        //    }
        //}
        //public double SummaryPrice
        //{
        //    get { return _sales.Select(l => l.CurrentPrice).Sum(); }
        //}

        public Series(string name)
        {
            Name = name;
            //_sales = new List<Sale>();
        }
        //public void AddSale(Sale sale)
        //{
        //    var lastOrDefault = _sales.LastOrDefault();
        //    if (lastOrDefault != null) sale.Number = lastOrDefault.Number + 1;

        //    //if (sale.Duration < Settings.MinSaleDuration)
        //    //{
        //    //    sale.Duration = Settings.MinSaleDuration;
        //    //}
        //    _sales.Add(sale);
        //}

        //public Sale GetSaleByNumber(int saleNumber)
        //{
        //    return _sales.FirstOrDefault(s => s.Number == saleNumber);
        //}

        //public double GetPrice() { return SummaryPrice; }
        //public double GetPriceByCategory(Category category)
        //{
        //    return _sales.Where(l => l.Category.Name == category.Name).Select(l => l.CurrentPrice).Sum();
        //}

        //public int GetActiveLotCout()
        //{
        //    return _sales.Count(l => l.IsActive);
        //}

        //public List<Buyer> GetBuyers()
        //{
        //    var buyers = new List<Buyer>();
        //    foreach (var lot in _sales)
        //    {
        //        if (!buyers.Contains<Buyer>(lot.Buyer))
        //            buyers.Add(lot.Buyer);
        //    }
        //    return buyers;
        //}
    }
}
