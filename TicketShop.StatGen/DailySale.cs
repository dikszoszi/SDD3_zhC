using System;

namespace TicketShop.StatGen
{
    public class DailySale
    {
        public DateTime Date { get; set; }
        public string SellerName { get; set; }
        public string SectorCode { get; set; }
        public int TicketsSold { get; set; }

        public override string ToString() => $" {this.TicketsSold} tickets sold to sector {this.SectorCode} on {this.Date} by seller {this.SellerName}.";
    }
}
