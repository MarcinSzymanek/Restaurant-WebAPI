using System.ComponentModel.DataAnnotations;

namespace HotelRestaurantAPI.Models;

public class Room
{
    [Key]
    public int RoomNumber { get; set; }
    public List<Guest> Guests { get; set; } = new();
    public List<CheckIn> CheckIns { get; set; } = new();
}