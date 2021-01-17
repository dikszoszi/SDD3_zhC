using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketShop.DB
{
    [Table("venues")]
    public class Venue
    {
        public Venue()
        {
            this.Sectors = new HashSet<Sector>();
            this.Sellers = new HashSet<Seller>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        
        [NotMapped]
        public virtual ICollection<Sector> Sectors { get; private set; }

        [NotMapped]
        public virtual ICollection<Seller> Sellers { get; private set; }
    }
}