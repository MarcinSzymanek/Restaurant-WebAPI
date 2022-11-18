// See https://aka.ms/new-console-template for more information

using System.Configuration;
using DeveloperApp.Tools;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;


Console.WriteLine("For first time setup, enter 'Initialize'");
Console.WriteLine("'quit' to Quit this app");
DbManager manager = new();

Console.WriteLine("Database manager loaded");

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
        // Use this command to update both databases in mssql
        // Also seeds DailyBreakfasts data
        // Does NOT seed users
        case "Initialize":
        {
            manager.InitSystem();
            break;
        }
        case "seed":
        {
            Console.WriteLine("Seeding data.");
            try
            {
                manager.Seeder.SeedBreakfasts(DateTime.Now.Day - 10);
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
            manager.ResetHotelDb();
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