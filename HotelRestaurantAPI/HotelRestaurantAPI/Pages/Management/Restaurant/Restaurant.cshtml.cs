using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using HotelRestaurantAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore.Internal;

namespace HotelRestaurantAPI.Pages.Management
{
    [Authorize(Policy = "RestaurantStaff")]
    public class RestaurantModel : PageModel
    {

        public IActionResult OnPostRedirect()
        {
            return RedirectToPage("DisplayRestaurantData");
        }

        private HotelDataContext _context;

        public RestaurantModel(HotelDataContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Input = new InputModel();
        }

        public async Task<IActionResult> OnPost()
        {
            
            Expected expected = new Expected();
            expected.RoomNumber = Input.RoomNumber;
            expected.Adults = Input.AdultAmount;
            expected.Children = Input.ChildrenAmount;
           
            return RedirectToPage("Succes");
        }

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            /*[Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = new DateTime(2022, 11, 14);*/

            [Required]
            [Display(Name = "Room number")]
            [Range(0, Double.PositiveInfinity)]
            public int RoomNumber { get; set; }

            [Required]
            [Display(Name = "Number of adults")]
            public int AdultAmount { get; set; } = 0;

            [Required]
            [Display(Name = "Number of children")]
            public int ChildrenAmount { get; set; } = 0;
        }
    }
}