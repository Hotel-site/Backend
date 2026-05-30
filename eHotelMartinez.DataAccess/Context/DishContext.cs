using eHotelMartinez.Domain.Entities.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.DataAccess.Context
{
    public class DishContext : DbContext
    {
        public DbSet<DishData> Dishes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishData>()
                .ToTable("Dishes");

            modelBuilder.Entity<DishData>()
                .HasIndex(d => new { d.DayOfWeek, d.Meal, d.Name })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
