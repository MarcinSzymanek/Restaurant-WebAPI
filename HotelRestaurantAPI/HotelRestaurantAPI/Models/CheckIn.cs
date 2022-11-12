using MessagePack;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace HotelRestaurantAPI.Models;

public class CheckIn
{
    public int Id { get; set; }
    [Required]
    public Room Room { get; set; }
    [Required]
    public Guest Guest { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public Reservation Reservation { get; set; }
}