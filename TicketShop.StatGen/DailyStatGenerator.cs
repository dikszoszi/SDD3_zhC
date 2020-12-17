using System;
using System.Collections.Generic;

namespace TicketShop.StatGen
{
    public class DailyStatGenerator
    {
        static readonly Random rnd = new Random();

        public string[] Sectors { get; set; }
        public string[] Sellers { get; set; }

        public DailyStatGenerator(string[] sectors, string[] sellers)
        {
            this.Sectors = sectors;
            this.Sellers = sellers;
        }

        /// <summary>
        /// The method that will return with a list of <see cref="DailySale"/> instances.
        /// <para>The <paramref name="numInstances"/> parameter will define howmany DailySale instances should be randomly generated; the <paramref name="numDays"/> defines the maximum number of days in the past that can be used for the random date generation.</para>
        /// the maxSold defines the biggest possible TicketsSold value in the random instances.Using these(and by randomly picking one seller and one sector from the ones received in the constructor) you should generate the list, filled with random ticket sale statistics.
        /// </summary>
        /// <param name="numDays">Defines the upper limit on days in the past that can be used for the random date generation.</param>
        /// <param name="numInstances">Defines how many DailySale instances should be randomly generated.</param>
        /// <param name="maxSold">Defines the largest possible TicketsSold value in the random instances.</param>
        /// <returns></returns>
        public List<DailySale> GenerateList(int numDays, int numInstances, int maxSold)
        {
            List<DailySale> dailySales = new List<DailySale>(numInstances);
            for (int i = 0; i < dailySales.Capacity; i++)
            {
                dailySales.Add(new DailySale
                {
                    Date = DateTime.Today.AddDays(rnd.Next((-1) * numDays, 1)),
                    TicketsSold = rnd.Next(1, maxSold + 1),
                    SectorCode = Sectors[rnd.Next(Sectors.Length)],
                    SellerName = Sellers[rnd.Next(Sellers.Length)]
                });
            }
            return dailySales;
        }
    }
}
