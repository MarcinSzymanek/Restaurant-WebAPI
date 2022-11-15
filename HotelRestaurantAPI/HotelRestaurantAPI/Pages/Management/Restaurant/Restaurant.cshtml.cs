using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using HotelRestaurantAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace HotelRestaurantAPI.Pages.Management
{
    [Authorize(Policy = "RestaurantStaff")]
    public class RestaurantModel : PageModel
    {
        private HotelDataContext _context;
        public RestaurantModel(HotelDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Restaurant(long? id)
        {
            if (id == null)
            {
                //return Page(new CheckedIn());
            }

            var dailyBreakfast = await _context.DailyBreakfasts.FindAsync(id);
            if (dailyBreakfast == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> Restaurant([Bind("RoomNumber,AdultAmount,ChildrenAmount")] CheckedIn checkInGuests)
        {
            DailyBreakfast addDailyBreakfast;
            var d = Input.Date.Day;
            var m = Input.Date.Month;
            try
            {
                addDailyBreakfast = await _context.DailyBreakfasts.Where(x => x.RoomNumber == checkInGuests.RoomNumber, d, m).FirstAsync();
            }

            catch
            {
                ModelState.AddModelError("RoomNumber", "No guests in this room");
                return Page();
            }

            OGBreakfastOrder.CheckedInAdults = checkInGuests.CheckedInAdults;
            OGBreakfastOrder.CheckedInChildren = checkInGuests.CheckedInChildren;

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(OGBreakfastOrder);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(HomeController.HomePage));
            }

            return Page(checkInGuests);
        }
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = new DateTime(2022, 11, 14);

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