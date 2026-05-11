using Microsoft.EntityFrameworkCore;
using eHotelMartinez.Domain.Entities.User;
using eHotelMartinez.Domain.Entities.Favorite;
using eHotelMartinez.Domain.Entities.Session;

namespace eHotelMartinez.DataAccess.Context
{
    public class UserContext : DbContext
    {
        public DbSet<UserData> Users { get; set; }
        public DbSet<FavoriteData> Favorites { get; set; }
        public DbSet<SessionData> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>()
                .ToTable("Users");

            modelBuilder.Entity<FavoriteData>()
                .ToTable("Favorites");

            modelBuilder.Entity<SessionData>()
                .ToTable("Sessions");

            modelBuilder.Entity<SessionData>()
                .HasIndex(s => s.SessionKey)
                .IsUnique();

            modelBuilder.Entity<FavoriteData>()
                .HasIndex(f => new { f.UserId, f.EntityType, f.EntityId })
                .IsUnique();

            modelBuilder.Entity<FavoriteData>()
                .HasOne<UserData>()
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
