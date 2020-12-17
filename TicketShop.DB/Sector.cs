using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketShop.DB
{
    [Table("sectors")]
    public class Sector
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Capacity { get; set; }

        [ForeignKey(nameof(Venue))]
        public int VenueId { get; set; }

        [MaxLength(13)]
        public string Code { get; set; }

        [NotMapped]
        public virtual Venue Venue { get; set; }

    }
}
