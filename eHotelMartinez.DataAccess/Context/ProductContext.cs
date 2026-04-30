using eHotelMartinez.Domain.Entities.Category;
using eHotelMartinez.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.DataAccess.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductData> Products { get; set; }
        public DbSet<CategoryData> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
