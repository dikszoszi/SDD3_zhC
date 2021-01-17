using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TicketShop.StatGen;

[assembly: System.CLSCompliant(false)]
namespace TicketShop.XML
{
    public static class XMLgeneration
    {
        /// <summary>
        /// Capable of generating an XML file from a list of type DailySale.
        /// </summary>
        /// <param name="dailySales">A list of DalySale instances</param>
        /// <returns>The XML file as a whole containing the necessary structure.</returns>
        public static XDocument GenerateXML(IEnumerable<DailySale> dailySales)
        {
            XDocument output = new XDocument(new XElement("stats"));

            var q1 = dailySales
                .GroupBy(singleStat => singleStat.Date)
                .OrderBy(dateGroup => dateGroup.Key);

            foreach (var grp in q1)
            {
                XElement node = new XElement("day");
                node.SetAttributeValue("date", grp.Key.Date.ToShortDateString());

                var q2 = grp.GroupBy(item => item.SectorCode).Select(sectorGrp =>
                    new XElement("sector", new XAttribute("code", sectorGrp.Key),
                            new XElement("sold", sectorGrp.Sum(x => x.TicketsSold))));

                var q3 = grp.GroupBy(item => item.SellerName).Select(sellerGrp =>
                    new XElement("seller", new XAttribute("name", sellerGrp.Key),
                            new XElement("sold", sellerGrp.Sum(x => x.TicketsSold))));

                q2.ToList().ForEach(subNode => node.Add(subNode));
                q3.ToList().ForEach(subNode => node.Add(subNode));
                output.Root.Add(node);
            }
            return output;
        }
    }

    public static class XmlGenerator
    {
        public static XDocument GenerateXml(IEnumerable<DailySale> list)
        {
            XDocument output = new XDocument(new XElement("stats"));

            var q1 = from singleStat in list
                     group singleStat by singleStat.Date into dateGroup
                     orderby dateGroup.Key
                     select dateGroup;
            foreach (var grp in q1)
            {
                XElement node = new XElement("day");
                node.SetAttributeValue("date", grp.Key.Date.ToShortDateString());

                var q2 = from item in grp
                         group item by item.SectorCode into sectorGrp
                         select new XElement("sector",
                            new XAttribute("code", sectorGrp.Key),
                            new XElement("sold", sectorGrp.Sum(x => x.TicketsSold)));
                var q3 = from item in grp
                         group item by item.SellerName into sellerGrp
                         select new XElement("seller",
                            new XAttribute("name", sellerGrp.Key),
                            new XElement("sold", sellerGrp.Sum(x => x.TicketsSold)));
                q2.ToList().ForEach(subNode => node.Add(subNode));
                q3.ToList().ForEach(subNode => node.Add(subNode));

                output.Root.Add(node);
            }
            return output;
        }
    }
}
