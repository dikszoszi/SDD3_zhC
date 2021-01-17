using System;
using System.Collections.Generic;
using System.Linq;
using TicketShop.DB;
using TicketShop.StatGen;
using TicketShop.XML;

[assembly: CLSCompliant(false)]
namespace TicketShop
{
    internal class Program
    {
        private static void Main()
        {
            TicketShopDbContext ctx = new TicketShopDbContext();

            ctx.Set<Venue>().Select(x => x.Name).ToConsole("VENUES");

            string[] sectors = ctx.Set<Sector>().Select(x => x.Code).ToArray();
            sectors.ToConsole("SECTORS");

            string[] sellers = ctx.Set<Seller>().Select(x => x.Name).ToArray();
            sellers.ToConsole("SELLERS");

            IEnumerable<DailySale> dailySales = new DailyStatGenerator(sectors, sellers).GenerateList(5, 20, 10);
            dailySales.ToConsole("LIST");

            //foreach (DailySale dailySale in dailySales) Console.WriteLine(dailySale.ToString());

            var stats = XMLgeneration.GenerateXML(dailySales);
            Console.WriteLine(stats);

            var perSector = stats.Descendants("sector")
                .GroupBy(sectorNode => sectorNode.Attribute("code").Value)
                .Select(grp => new { Sector = grp.Key, TotalSold = grp.Sum(x => (int)x.Element("sold")) });

            var perSeller = stats.Descendants("seller")
                .GroupBy(sellerNode => sellerNode.Attribute("name").Value)
                .OrderBy(grp => grp.Key)
                .Select(grp => new { Seller = grp.Key, TotalSold = grp.Sum(x => (int)x.Element("sold")) });

            // Without ToList() ??? :) 
            var remainingSeats = ctx.Set<Sector>().ToList().Select(sector => new
            {
                Total = sector.Capacity,
                Remaining = sector.Capacity - perSector.SingleOrDefault(stat => stat.Sector == sector.Code).TotalSold
            });

            perSector.ToConsole("PerSector");
            perSeller.ToConsole("PerSeller");
            remainingSeats.ToConsole("RemainingSeats");

            Console.WriteLine("ALL OK: " + remainingSeats.All(x => x.Remaining > 0));
            Console.WriteLine("ALL OK: " + (!remainingSeats.Any(x => x.Remaining < 0)));

            ctx.Dispose();
        }
    }
}
