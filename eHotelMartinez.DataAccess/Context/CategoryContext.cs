using eHotelMartinez.Domain.Entities.Category;
using eHotelMartinez.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.DataAccess.Context
{
    public class CategoryContext : DbContext
    {
        public DbSet<CategoryData> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryData>()
                .ToTable("Categories")
                .HasQueryFilter(c => c.IsActive);

            modelBuilder.Entity<CategoryData>()
                .HasIndex(c => c.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
