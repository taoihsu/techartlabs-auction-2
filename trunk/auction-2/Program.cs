using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using auction_2.Events;

namespace auction_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new AuctionSettings();
            // Создаем аукцион и регистрируем продавцов и покупателей
            var auction = new Auction();
            //var bidSignaler = new Thread(BidSignal);
            //bidSignaler.Start(auction);


            auction.AddBuyer(new Buyer("blogin1", "Fred", "Durst"));
            auction.AddBuyer(new Buyer("blogin2", "Samuel", "Rivers"));
            auction.AddBuyer(new Buyer("blogin3", "John", "Otto"));
            auction.AddBuyer(new Buyer("blogin4", "Wesley", "Borland"));
            auction.AddSeller(new Seller("slogin1", "Sid", "Wilson"));
            auction.AddSeller(new Seller("slogin2", "Jonas", "Jordison"));
            auction.AddSeller(new Seller("slogin3", "Chris", "Fehn"));
            auction.AddSeller(new Seller("slogin4", "James", "Root"));


            auction.AddCategory(new Category("Elite"));
            auction.AddCategory(new Category("Nom-nom"));
            auction.AddSeries(new Series("Musical Instruments"));
            auction.AddSeries(new Series("Fruits"));

            var sale1 = new Sale("Hamer Standard guitar USA 1976",
                                  new Lot("Hamer Standard guitar USA 1976", "Super Guitar!", null),
                                  auction.Series.FirstOrDefault(s => s.Name == "Musical Instruments"),
                                  auction.Sellers.First(s => s.Login == "slogin2"), 101, 3, TimeSpan.FromSeconds(1.1),
                                  auction.Categories.FirstOrDefault(c => c.Name == "Elite"));
            var sale2 = new Sale("Apple",
                                  new Lot("Green apple", "=^__^=", null),
                                  auction.Series.FirstOrDefault(s => s.Name == "Fruits"),
                                  auction.Sellers.First(s => s.Login == "slogin1"), 10, 1, TimeSpan.FromSeconds(4),
                                  auction.Categories.FirstOrDefault(c => c.Name == "Nom-nom"));
            auction.AddSale(sale1);
            auction.AddSale(sale2);

            //var waitForFinish = new Thread(FinishMessage);
            //waitForFinish.Start(sale1);

            var b1 = new Bid(sale1, 105, auction.Buyers.First(b => b.Login == "blogin1"));
            var b2 = new Bid(sale1, 110, auction.Buyers.First(b => b.Login == "blogin1"));
            var b3 = new Bid(sale1, 109, auction.Buyers.First(b => b.Login == "blogin2"));
            var b4 = new Bid(sale1, 111, auction.Buyers.First(b => b.Login == "blogin3"));
            var b5 = new Bid(sale1, 113, auction.Buyers.First(b => b.Login == "blogin4"));
            var b6 = new Bid(sale1, 120, auction.Buyers.First(b => b.Login == "blogin2"));
            var b7 = new Bid(sale1, 125, auction.Buyers.First(b => b.Login == "blogin3"));
            var b8 = new Bid(sale1, 150, auction.Buyers.First(b => b.Login == "blogin1"));

            var b9 = new Bid(sale2, 15, auction.Buyers.First(b => b.Login == "blogin1"));
            var b10 = new Bid(sale2, 23, auction.Buyers.First(b => b.Login == "blogin3"));
            var b11 = new Bid(sale2, 29, auction.Buyers.First(b => b.Login == "blogin1"));




            sale1.BidMaked += ReportBid;
            sale2.BidMaked += ReportBid;
            //sale1.SaleFinished += ReportSaleFinish;
            //sale2.SaleFinished += ReportSaleFinish;
            auction.SaleFinished += ReportSaleFinish;

            auction.MakeBid(b1);
            Thread.Sleep(500);
            auction.MakeBid(b2);    auction.MakeBid(b9);
            Thread.Sleep(500);
            auction.MakeBid(b3);
            Thread.Sleep(500);
            auction.MakeBid(b4);    auction.MakeBid(b10);
            Thread.Sleep(500);
            auction.MakeBid(b5);    auction.MakeBid(b11);
            Thread.Sleep(500);
            auction.MakeBid(b6);
            Thread.Sleep(500);
            auction.MakeBid(b7);
            Thread.Sleep(500);
            auction.MakeBid(b8);
            Thread.Sleep(500);

            var activeBuyers = auction.GetActiveBuyers(100);
            foreach (var activeBuyer in activeBuyers)
            {
                Console.WriteLine(activeBuyer.Login);
            }

        }

        public static void ReportBid(object sender, EventArgs<Bid> args)
        {
            var bid = args.EventInfo;
            Console.WriteLine("bid:\t{0}\tbidder:\t{1}\tlot:\t{2}", bid.Value,
                                         bid.Bidder.Login, bid.Sale.Name);
        }

        public static void ReportSaleFinish(object sender, EventArgs<Sale> args)
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
