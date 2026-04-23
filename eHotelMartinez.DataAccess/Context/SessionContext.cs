using eHotelMartinez.Domain.Entities.Session;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.DataAccess.Context
{
    public class SessionContext : DbContext
    {
        public DbSet<SessionData> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionData>().ToTable("Sessions");
            modelBuilder.Entity<SessionData>().HasIndex(s => s.SessionKey).IsUnique();
        }
    }
}
