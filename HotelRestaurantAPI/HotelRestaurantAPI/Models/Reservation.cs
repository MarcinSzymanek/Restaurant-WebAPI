using System.ComponentModel.DataAnnotations;

namespace HotelRestaurantAPI.Models;

public class Reservation
{
    public DateTime Date { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int GAmount { get; set; } = 0;
    public List<CheckIn> CheckIns { get; set; } = new();
}