using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelRestaurantAPI.Pages.Management
{
    [Authorize("ReceptionStaff")]
    public class ReceptionModel : PageModel
    {
       
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return RedirectToPage();
        }

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = new DateTime(2022, 11, 12);
            [Required]
            public int GuestAmount { get; set; }
        }
    }
}
