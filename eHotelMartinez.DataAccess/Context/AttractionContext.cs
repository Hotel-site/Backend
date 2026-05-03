using eHotelMartinez.Domain.Entities.Attraction;
using eHotelMartinez.Domain.Entities.Category;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.DataAccess.Context
{
    public class AttractionContext : DbContext
    {
        public DbSet<AttractionData> Attractions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                .OwnsMany(a => a.OpeningHours, OpeningHour =>
                {
                    OpeningHour.Property(oh => oh.DayOfWeek).HasColumnName("DayOfWeek");
                    OpeningHour.Property(oh => oh.Start).HasColumnName("Start");
                    OpeningHour.Property(oh => oh.End).HasColumnName("End");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
