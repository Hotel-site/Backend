using eHotelMartinez.Domain.Entities.Favorite;
using eHotelMartinez.Domain.Entities.Order;
using eHotelMartinez.Domain.Entities.Session;
using eHotelMartinez.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.DataAccess.Context
{
    public class UserContext : DbContext
    {
        public DbSet<UserData> Users { get; set; }
        public DbSet<FavoriteData> Favorites { get; set; }
        public DbSet<SessionData> Sessions { get; set; }
        public DbSet<OrderData> Orders { get; set; }
        public DbSet<OrderItemData> OrderItems { get; set; }

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

            modelBuilder.Entity<OrderData>()
                .ToTable("Orders");

            modelBuilder.Entity<SessionData>()
                .HasIndex(s => s.SessionKey)
                .IsUnique();

            modelBuilder.Entity<SessionData>()
                .HasOne<UserData>()
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteData>()
                .HasIndex(f => new { f.UserId, f.EntityType, f.EntityId })
                .IsUnique();

            modelBuilder.Entity<FavoriteData>()
                .HasOne<UserData>()
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderData>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItemData>()
                .HasOne(i => i.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
