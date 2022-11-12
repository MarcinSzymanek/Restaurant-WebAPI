// See https://aka.ms/new-console-template for more information

using System.Configuration;
using DeveloperApp.Tools;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

GuestAdult g1 = new GuestAdult() { FirstName = "Janek", LastName = "Kowalski", PhoneNumber = "42424242" };

Console.WriteLine("Hello, World!");

DbManager manager = new();

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
            try
            {
                manager.Seeder.SeedRooms();
            }
            catch(Exception ex)
            {
                Console.WriteLine("nuh-uh");
                Console.WriteLine(ex.ToString());
            }
            break;
        }
        case "reset":
        {
            Console.WriteLine("Resetting HotelDbContext");
            // Add code to delete all entries from db here
            break;
        }
        case "rooms":
        {
            var rooms = await manager.Context.Rooms.ToListAsync();
            foreach (var r in rooms)
            {
                Console.WriteLine("Room: " + r.RoomNumber + " exists!");
            }

            break;
        }
        case "reservations":
        {
            var res = await manager.GetAll<Reservation>();
            foreach (var r in res)
            {
                Console.WriteLine(r.Day);
            }
            break;
        }
        case "guests":
        {
            var guests = await manager.Context.Guests.ToListAsync();
            foreach (var g in guests)
            {
                Console.WriteLine(g.FirstName + " " + g.LastName + " Id:" + g.Id);
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