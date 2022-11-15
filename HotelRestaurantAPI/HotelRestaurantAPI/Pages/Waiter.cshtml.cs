using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace HotelRestaurantAPI.Pages


{

    [Authorize(Policy="WaiterStaff")]
    public class WaiterModel : PageModel
    {

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = new DateTime(2022, 11, 14);

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
