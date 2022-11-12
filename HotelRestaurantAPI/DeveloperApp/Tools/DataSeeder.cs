using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperApp.Tools;

public class DataSeeder
{
    private HotelDataContext _context;
    public DataSeeder(HotelDataContext context)
    {
        _context = context;
    }
    /*
     * All seeding functions go into this class
     */
    public void SeedReservations(int startDay)
    {
        List<Reservation> reservations = new();
        for (int i = startDay; i < startDay + 7; i++)
        {
            var r = new Reservation() { Day = i, Month = 11 };
            reservations.Add(r);
        }

        foreach (var r in reservations)
        {
            _context.Reservations.Add(r);
            _context.SaveChanges();
        }
    }

    public void SeedRooms()
    {
        int index = 101;
        List<Room> rooms = new();
        for (int i = index; i < index + 7; i++)
        {
            var r = new Room() { RoomNumber = i};
            rooms.Add(r);
        }

        foreach (var r in rooms)
        {
            using (var t = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Rooms ON;");
                _context.Rooms.Add(r);
                _context.SaveChanges();
                t.Commit();
            }
            
        }
    }
}