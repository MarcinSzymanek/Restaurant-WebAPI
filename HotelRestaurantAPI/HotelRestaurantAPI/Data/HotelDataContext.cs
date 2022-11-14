using HotelRestaurantAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRestaurantAPI.Data;

public class HotelDataContext : DbContext
{
    public HotelDataContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyBreakfast>().HasKey(k => new { k.Day, k.Month });

        modelBuilder.Entity<DailyBreakfast>().HasMany(b => b.CheckedIn)
            .WithOne(c => c.DailyBreakfast);
        modelBuilder.Entity<DailyBreakfast>().HasOne(b => b.Expected)
            .WithOne(e => e.DailyBreakfast)
            .HasForeignKey<Expected>(e => new { e.Day, e.Month });

        modelBuilder.Entity<CheckedIn>()
            .HasKey(c => c.RoomNumber);

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<DailyBreakfast> DailyBreakfasts { get; set; }
}