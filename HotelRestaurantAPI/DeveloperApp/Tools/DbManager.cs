using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;

namespace DeveloperApp.Tools;

/// <summary>
/// A tool for managing databases for our project
/// Can Initialize (update database) both databases
/// Can seed initial breakfast data
/// Can reset hotel data (delete all entries)
/// </summary>
public class DbManager
{
    HotelDataContext _hotelContext;
    private ApplicationDbContext _staffContext;
    public HotelDataContext Context
    {
        get => _hotelContext;
    }
    public DataSeeder _seeder;
    public DataSeeder Seeder
    {
        get => _seeder;
    }

    public DbManager()
    {
        _hotelContext = SetupHotelContext();
        _staffContext = SetupStaffContext();
        _seeder = new(_hotelContext);
    }

    /// <summary>
    /// Update both databases with correct migrations
    /// Seed 30 days from 10 days ago until now (startdata)
    /// </summary>
    public void InitSystem()
    {
        Console.WriteLine("Setting up the system.");
        Console.WriteLine("Migrating staff database");
        _staffContext.Database.Migrate();
        Console.WriteLine("Migrating hotel database");
        _hotelContext.Database.Migrate();
        Console.WriteLine("Seeding initial breakfast data 30 days.");
        _seeder.SeedBreakfasts(DateTime.Now.Day-10);
        Console.WriteLine("Done.");
    }

    /// <summary>
    /// Remove all data from Hotel database
    /// Doesn't remove staff user data
    /// </summary>
    /// <returns></returns>
    public bool ResetHotelDb()
    {
        var breakfasts = _hotelContext.DailyBreakfasts.ToList();
        
        try
        {
            foreach (var b in breakfasts)
            {
                _hotelContext.DailyBreakfasts.Remove(b);
                _hotelContext.SaveChanges();
            }
            var checkins = _hotelContext.CheckedIn.ToList();
            foreach (var c in checkins)
            {
                _hotelContext.CheckedIn.Remove(c);
                _hotelContext.SaveChanges();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Unused. Get specific entry of type T by key
    /// </summary>
    /// <typeparam name="T">Type of the entry requested</typeparam>
    /// <param name="key">Key of the entry</param>
    /// <returns></returns>
    public T Get<T>(object[] key) where T : class
    {
        var result = _hotelContext.Find<T>(key);
        return result;
    }

    /// <summary>
    /// Setup hotel database context
    /// </summary>
    /// <returns>Hotel DbContext</returns>
    public HotelDataContext SetupHotelContext()
    {
        var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false);
        var c = configBuilder.Build();
        var hotelConnString = c.GetConnectionString("hotelDbConnection");
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseSqlServer(hotelConnString);
        var context = new HotelDataContext(optionsBuilder.Options);
        return context;
    }

    /// <summary>
    /// Setup staff db context
    /// </summary>
    /// <returns>Staff dbContext</returns>
    private ApplicationDbContext SetupStaffContext()
    {
        var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false);
        var c = configBuilder.Build();
        var hotelConnString = c.GetConnectionString("staffDbConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(hotelConnString);
        var context = new ApplicationDbContext(optionsBuilder.Options);
        return context;
    }
}