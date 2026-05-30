using eHotelMartinez.Domain.Entities.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace eHotelMartinez.DataAccess.Context
{
    public class RoomContext : DbContext
    {
        public DbSet<RoomData> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            var amenitiesConverter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v ?? new List<string>(), typeof(List<string>), (JsonSerializerOptions?)null),
            v => string.IsNullOrWhiteSpace(v)
             ? new List<string>()
             : (JsonSerializer.Deserialize(v, typeof(List<string>), (JsonSerializerOptions?)null) as List<string> ?? new List<string>())
            );

            var amenitiesComparer = new ValueComparer<List<string>>(
                (a, b) => (a ?? new List<string>()).SequenceEqual(b ?? new List<string>()),
                a => (a ?? new List<string>()).Aggregate(0, (hash, item) => HashCode.Combine(hash, item)),
                a => (a ?? new List<string>()).ToList()
            );

            modelBuilder.Entity<RoomData>()
                .Property(r => r.Amenities)
                .HasConversion(amenitiesConverter)
                .Metadata.SetValueComparer(amenitiesComparer);

            modelBuilder.Entity<RoomData>()
                .Property(r => r.Amenities)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("Amenities");

            modelBuilder.Entity<RoomData>()
                .HasMany(r => r.Images)
                .WithOne(i => i.Room)
                .HasForeignKey(i => i.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoomData>()
                .Property(r => r.Price)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
