using System;
using System.Collections.Generic;

[assembly: CLSCompliant(false)]
namespace TicketShop.StatGen
{
    public class DailyStatGenerator
    {
        static readonly Random rnd = new Random();

        public IList<string> Sectors { get; private set; }
        public IList<string> Sellers { get; private set; }

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
        public IEnumerable<DailySale> GenerateList(int numDays, int numInstances, int maxSold)
        {
            for (int i = 0; i < numInstances; i++)
            {
                yield return new DailySale
                {
                    Date = DateTime.Today.AddDays(rnd.Next(-numDays, 1)),
                    TicketsSold = rnd.Next(1, maxSold + 1),
                    SectorCode = Sectors[rnd.Next(Sectors.Count)],
                    SellerName = Sellers[rnd.Next(Sellers.Count)]
                };
            }
        }
    }
}
