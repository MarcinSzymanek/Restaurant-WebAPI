using System.ComponentModel.DataAnnotations;

namespace HotelRestaurantAPI.Models;

public class GuestAdult : Guest
{
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }   
}