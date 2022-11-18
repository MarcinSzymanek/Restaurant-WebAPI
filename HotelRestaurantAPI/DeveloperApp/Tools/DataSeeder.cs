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
    

    public void SeedBreakfasts(int startDay)
    {
        List<DailyBreakfast> breakfasts = new();
        for (int i = startDay; i < startDay + 30; i++)
        {
            int d = i;
            int m = DateTime.Now.Month;
            if (i > 31)
            {
                d -= 31;
                m++;
            }
            var b = new DailyBreakfast() { Day = d, Month = m };
            breakfasts.Add(b);
        }

        foreach (var b in breakfasts)
        {
            _context.DailyBreakfasts.Add(b);
            _context.SaveChanges();
        }
    }
    
}