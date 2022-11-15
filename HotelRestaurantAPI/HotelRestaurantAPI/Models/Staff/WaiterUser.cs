using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace HotelRestaurantAPI.Models.Staff;


public class WaiterUser : BaseUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    



}