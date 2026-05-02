using eHotelMartinez.Domain.Entities.Attraction;
using eHotelMartinez.Domain.Entities.Category;
using eHotelMartinez.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.DataAccess.Context
{
    public class AttractionContext : DbContext
    {
        public DbSet<AttractionData> Attractions { get; set; }
        public DbSet<CategoryData> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }


    }
}
