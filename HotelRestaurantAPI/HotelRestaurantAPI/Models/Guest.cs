using System.ComponentModel.DataAnnotations;

namespace HotelRestaurantAPI.Models;

public abstract class Guest
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string LastName { get; set; }
    public Room Room { get; set; }
}