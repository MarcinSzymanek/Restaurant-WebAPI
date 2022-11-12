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
        modelBuilder.Entity<Reservation>()
            .HasKey(k => new { k.Day, k.Month });
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Room> Rooms { get; set; }
    
    public DbSet<Guest> Guests { get; set; }
    public DbSet<GuestAdult> Adults { get; set; }
    public DbSet<GuestChild> Children { get; set; }

    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<CheckIn> CheckIns { get; set; }
}