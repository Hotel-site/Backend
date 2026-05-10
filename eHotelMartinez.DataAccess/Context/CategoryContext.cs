using eHotelMartinez.Domain.Entities.Category;
using eHotelMartinez.Domain.Entities.Product;
using eHotelMartinez.Domain.Entities.Attraction;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.DataAccess.Context
{
    public class CategoryContext : DbContext
    {
        public DbSet<CategoryData> Categories { get; set; }
        public DbSet<ProductData> Products { get; set; }
        public DbSet<AttractionData> Attractions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Category configuration
            modelBuilder.Entity<CategoryData>()
                .ToTable("Categories")
                .HasQueryFilter(c => c.IsActive);

            modelBuilder.Entity<CategoryData>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Attraction configuration
            modelBuilder.Entity<AttractionData>()
                .HasMany(a => a.Images)
                .WithOne()
                .HasForeignKey(i => i.AttractionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttractionData>()
                .OwnsOne(a => a.Contacts, PartnerContacts =>
                {
                    PartnerContacts.Property(c => c.Email).HasColumnName("Email");
                    PartnerContacts.Property(c => c.Phone).HasColumnName("Phone");
                    PartnerContacts.Property(c => c.BookingUrl).HasColumnName("BookingUrl");
                });

            modelBuilder.Entity<AttractionData>()
                .OwnsOne(a => a.Location, Location =>
                {
                    Location.Property(l => l.Address)
                    .HasColumnName("Address");

                    Location.Property(l => l.Latitude)
                    .HasColumnName("Latitude")
                    .HasColumnType("decimal(10,7)");

                    Location.Property(l => l.Longitude)
                    .HasColumnName("Longitude")
                    .HasColumnType("decimal(10,7)");
                });

            modelBuilder.Entity<AttractionData>()
                .HasMany(a => a.OpeningHours)
                .WithOne()
                .HasForeignKey(oh => oh.AttractionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttractionData>()
                .HasOne<CategoryData>()
                .WithMany()
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product configuration
            modelBuilder.Entity<ProductData>()
                .HasMany(p => p.Images)
                .WithOne()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductData>()
                .HasOne<CategoryData>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
