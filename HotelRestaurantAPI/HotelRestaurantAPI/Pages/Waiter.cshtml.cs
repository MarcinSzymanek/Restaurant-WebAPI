using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

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

        private readonly IHubContext<KitchenService, IKitchenService> _kitchenContext;
        
        public WaiterModel(IReservationService reservationService, HotelDataContext context, IHubContext<KitchenService, IKitchenService> kitchenContext )
        {
            _reservationService = reservationService;
            _context = context;
            _kitchenContext = kitchenContext;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dateNow = DateTime.Now;

          
            bool success = await _reservationService.AddCheckedIn(_context, dateNow.Day, dateNow.Month, Input.RoomNumber, Input.AdultAmount, Input.ChildrenAmount);
            if (success)
            {
                ReservationMessage = "Success!";
                _kitchenContext.Clients.All.KitchenUpdate();
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
