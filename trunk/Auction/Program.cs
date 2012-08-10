using System;
using System.Linq;
using System.Threading;
using Auction.Events;
using Auction.Users;

namespace Auction
{
    class Program
    {
        static void Main()
        {
            var testAuction = new Auction();

            testAuction.AddBuyer(new Buyer("blogin1", "Fred", "Durst"));
            testAuction.AddBuyer(new Buyer("blogin2", "Samuel", "Rivers"));
            testAuction.AddBuyer(new Buyer("blogin3", "John", "Otto"));
            testAuction.AddBuyer(new Buyer("blogin4", "Wesley", "Borland"));
            testAuction.AddSeller(new Seller("slogin1", "Sid", "Wilson"));
            testAuction.AddSeller(new Seller("slogin2", "Jonas", "Jordison"));
            testAuction.AddSeller(new Seller("slogin3", "Chris", "Fehn"));
            testAuction.AddSeller(new Seller("slogin4", "James", "Root"));

            testAuction.AddCategory(new Category("Elite"));
            testAuction.AddCategory(new Category("Nom-nom"));
            testAuction.AddSeries(new Series("Musical Instruments"));
            testAuction.AddSeries(new Series("Fruits"));

            var sale1 = new Sale("Hamer Standard guitar USA 1976",
                                  new Lot("Hamer Standard guitar USA 1976", "Super Guitar!", null),
                                  testAuction.Series.FirstOrDefault(s => s.Name == "Musical Instruments"),
                                  testAuction.Sellers.First(s => s.Login == "slogin2"), 101, 3, TimeSpan.FromSeconds(1.1),
                                  testAuction.Categories.FirstOrDefault(c => c.Name == "Elite"));
            var sale2 = new Sale("Apple",
                                  new Lot("Green apple", "=^__^=", null),
                                  testAuction.Series.FirstOrDefault(s => s.Name == "Fruits"),
                                  testAuction.Sellers.First(s => s.Login == "slogin1"), 10, 1, TimeSpan.FromSeconds(4),
                                  testAuction.Categories.FirstOrDefault(c => c.Name == "Nom-nom"));
            testAuction.AddSale(sale1);
            testAuction.AddSale(sale2);

            var b1 = new Bid(sale1, 105, testAuction.Buyers.First(b => b.Login == "blogin1"));
            var b2 = new Bid(sale1, 110, testAuction.Buyers.First(b => b.Login == "blogin1"));
            var b3 = new Bid(sale1, 109, testAuction.Buyers.First(b => b.Login == "blogin2"));
            var b4 = new Bid(sale1, 111, testAuction.Buyers.First(b => b.Login == "blogin3"));
            var b5 = new Bid(sale1, 113, testAuction.Buyers.First(b => b.Login == "blogin4"));
            var b6 = new Bid(sale1, 120, testAuction.Buyers.First(b => b.Login == "blogin2"));
            var b7 = new Bid(sale1, 125, testAuction.Buyers.First(b => b.Login == "blogin3"));
            var b8 = new Bid(sale1, 150, testAuction.Buyers.First(b => b.Login == "blogin1"));

            var b9 = new Bid(sale2, 15, testAuction.Buyers.First(b => b.Login == "blogin1"));
            var b10 = new Bid(sale2, 23, testAuction.Buyers.First(b => b.Login == "blogin3"));
            var b11 = new Bid(sale2, 29, testAuction.Buyers.First(b => b.Login == "blogin1"));




            sale1.BidMaked += ReportBid;
            sale2.BidMaked += ReportBid;

            testAuction.SaleFinished += ReportSaleFinish;

            testAuction.MakeBid(b1);
            Thread.Sleep(500);
            testAuction.MakeBid(b2);    testAuction.MakeBid(b9);
            Thread.Sleep(500);
            testAuction.MakeBid(b3);
            Thread.Sleep(500);
            testAuction.MakeBid(b4);    testAuction.MakeBid(b10);
            Thread.Sleep(500);
            testAuction.MakeBid(b5);    testAuction.MakeBid(b11);
            Thread.Sleep(500);
            testAuction.MakeBid(b6);
            Thread.Sleep(500);
            testAuction.MakeBid(b7);
            Thread.Sleep(500);
            testAuction.MakeBid(b8);
            Thread.Sleep(500);

            //active Buyers (50%)
            Console.WriteLine("\nActive Buyers:");
            var activeBuyers = testAuction.GetActiveBuyers(50);
            foreach (var activeBuyer in activeBuyers)
            {
                Console.WriteLine(activeBuyer.Login);
            }

        }

        public static void ReportBid(object sender, ActionEventArgs<Bid> args)
        {
            var bid = args.EventInfo;
            Console.WriteLine("bid:\t{0}\tbidder:\t{1}\tlot:\t{2}", bid.Value,
                                         bid.Bidder.Login, bid.Sale.Name);
        }

        public static void ReportSaleFinish(object sender, ActionEventArgs<Sale> args)
        {
            var sale = args.EventInfo;
            if (sale.LastBid != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("sale #" + sale.Number + " finished!\tWinnwer:\t" + (sale.Buyer == null ? "none" : sale.Buyer.Login) + "\tPRICE: "
                + sale.CurrentPrice);
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("sale #" + sale.Number + " finished with no bids :(");
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        /* Старые методы проверки
        public static void BidSignal(object auction)
        {
            var auc = (Auction) auction;
            var bidCount = auc.Bids.Count;
            while (true)
            {
                if (auc.Bids.Count != bidCount)
                {
                    Console.WriteLine("bid:\t{0}\tbidder:\t{1}\tlot:\t{2}", auc.Bids.Last().Value,
                                      auc.Bids.Last().Bidder.Login, auc.Bids.Last().Sale.Name);
                    bidCount = auc.Bids.Count;
                }
                Thread.Sleep(100);
            }
        }

        public static void FinishMessage(object saleArg)
        {
            var sale = (Sale)saleArg;
            var timeout = sale.FinishTime - DateTime.Now + TimeSpan.FromSeconds(0.001);
            Thread.Sleep(timeout);
            Console.WriteLine("sale #" + sale.Number + " finished!\tWinnwer:\t" + (sale.Buyer == null ? "none" : sale.Buyer.Login) + "\tPRICE: "
            + sale.CurrentPrice);
        }
         */
    }
}
