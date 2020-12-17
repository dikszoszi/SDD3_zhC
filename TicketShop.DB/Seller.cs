﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketShop.DB
{
    [Table("sellers")]
    public class Seller
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Venue))]
        public int VenueId { get; set; }

        [NotMapped]
        public virtual Venue Venue { get; set; }
    }
}
