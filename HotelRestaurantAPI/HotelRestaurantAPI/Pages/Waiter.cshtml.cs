using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelRestaurantAPI.Pages
{
  

    [Authorize(Policy="WaiterStaff")]
    public class WaiterModel : PageModel
    {
        private DateTime _now = DateTime.Now;
        private int _day = DateTime.Now.Day;
        private int _month = DateTime.Now.Month;


        IReservationService _reservationService;
        HotelDataContext _context;


        public WaiterModel(IReservationService reservationService, HotelDataContext context)
        {
            _reservationService = reservationService;
            _context = context;
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dateNow = DateTime.Now;

          
            bool success = await _reservationService.AddCheckedIn(_context, dateNow.Day, dateNow.Month, Input.RoomNumber, Input.AdultAmount, Input.ChildrenAmount);
            if (success)
            {
                ReservationMessage = "Success!";
            }
            else
            {
                ReservationMessage = "Error saving data to the database!";
            }

            return Page();



        }
        



        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
        

            public int AdultAmount { get; set; } = 0;
            public int ChildrenAmount { get; set; } = 0;

            public int RoomNumber { get; set; } = 0;
        }

        public string ReservationMessage { get; set; } = "";

        public void OnGet()
        {
            
        }
    }
}
