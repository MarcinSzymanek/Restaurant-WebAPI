// See https://aka.ms/new-console-template for more information

using System.Configuration;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

Room r404 = new Room() { RoomNumber = 404 };

Console.WriteLine("Hello, World!");

var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json",false);
var c = configBuilder.Build();
var hotelConnString = c.GetConnectionString("hotelDbConnection");
var optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseSqlServer(hotelConnString);
var context = new HotelDataContext(optionsBuilder.Options);

Console.WriteLine("Database loaded");

bool done = false;
while (!done)
{
    string input = Console.ReadLine();
    
    switch(input)
    {
        case "quit":
        {
            done = true;
            break;
        }
        case "seed":
        {
            Console.WriteLine("Seeding data.");
            using (var transaction = context.Database.BeginTransaction())
            {
                context.Rooms.Add(r404);
                // Only need to do this once
                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Rooms ON;");
                var success = await context.SaveChangesAsync();
                if (success != -1)
                {
                    Console.WriteLine("Seems to work");
                    transaction.Commit();
                    break;
                }
            }
            Console.WriteLine("nuh-uh");
            break;
        }
        case "reset":
        {
            Console.WriteLine("Resetting HotelDbContext");
            break;
        }
        case "rooms":
        {
            var rooms = await context.Rooms.ToListAsync();
            foreach (var r in rooms)
            {
                Console.WriteLine("Room: " + r.RoomNumber + " exists!");
            }

            break;
        }
        default:
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            break;
        }
    }
}