using HotelRestaurantAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRestaurantAPI.Data;

public class HotelDataContext : DbContext
{
    public HotelDataContext(DbContextOptions options) : base(options)
    {

    }


    public DbSet<Room> Rooms { get; set; }
    public DbSet<GuestChild> GuestChildren { get; set; }
    public DbSet<GuestAdult> GuestAdults { get; set; }

    public DbSet<CheckIn> CheckIns { get; set; }
}