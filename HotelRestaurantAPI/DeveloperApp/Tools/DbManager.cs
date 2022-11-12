using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;

namespace DeveloperApp.Tools;

public class DbManager
{
    HotelDataContext _hotelContext;
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
        _seeder = new(_hotelContext);

    }
    public T Get<T>(object[] key) where T : class
    {
        var result = _hotelContext.Find<T>(key);
        return result;
    }

    public async Task<List<T>?> GetAll<T>() where T : class
    {
        Type r = typeof(Reservation);
        Type g = typeof(Guest);
        Type c = typeof(CheckIn);
        Type type = typeof(T);
        List<T> result;
        if (type == r)
        {
            result = new List<T>();
            var res = await _hotelContext.Reservations.ToListAsync();
            try
            {
                foreach (Reservation reservation in res)
                {
                    result.Add(reservation as T);
                }
            }
            catch
            {
                Console.WriteLine("Types incompatible!!!");
                Console.WriteLine("Manager tool failure.");
                Environment.Exit(-1);
            }
            return result;
        }

        return null;
    }

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
}