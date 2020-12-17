using Microsoft.EntityFrameworkCore;

namespace TicketShop.DB
{
    public partial class TicketShopDbContext : DbContext
    {
        public virtual DbSet<Venue> Venues { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Seller> Sellers { get; set; }

        public TicketShopDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\TicketShopDb.mdf; Integrated Security=True; MultipleActiveResultSets = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasOne(sector => sector.Venue)
                .WithMany(venue => venue.Sectors)
                .HasForeignKey(sector => sector.VenueId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasOne(seller => seller.Venue)
                .WithMany(venue => venue.Sellers)
                .HasForeignKey(seller => seller.VenueId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
            Venue venue1 = new Venue() { Id = 1, Name = "Papp László Sports Arena" };
            Sector sector1 = new Sector() { Id = 1, Code = "A", Capacity = 1100, VenueId = venue1.Id };
            Sector sector2 = new Sector() { Id = 2, Code = "B", Capacity = 2500, VenueId = venue1.Id };
            Sector sector3 = new Sector() { Id = 3, Code = "C", Capacity = 1500, VenueId = venue1.Id };
            Seller seller1 = new Seller() { Id = 1, Name = "Broadway", VenueId = venue1.Id };
            Seller seller2 = new Seller() { Id = 2, Name = "Eventim", VenueId = venue1.Id };

            modelBuilder.Entity<Venue>().HasData(venue1);
            modelBuilder.Entity<Sector>().HasData(sector1, sector2, sector3);
            modelBuilder.Entity<Seller>().HasData(seller1, seller2);
        }
    }
}
