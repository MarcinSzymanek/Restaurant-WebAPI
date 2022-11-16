using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using Microsoft.AspNetCore.Authorization;
using HotelRestaurantAPI.Services;
using Microsoft.AspNetCore.SignalR;

namespace HotelRestaurantAPI.Pages.Management
{
    [Authorize(Policy = "WaiterStaff")]
    public class CheckinGuestModel : PageModel
    {
        private readonly HotelRestaurantAPI.Data.HotelDataContext _context;

       private readonly IHubContext<KitchenService, IKitchenService> _kitchenContext;
        public CheckinGuestModel(HotelRestaurantAPI.Data.HotelDataContext context, IHubContext<KitchenService, IKitchenService> kitchenContext)
        {
            _context = context;
            _kitchenContext = kitchenContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CheckedIn CheckedIn { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CheckedIn.Add(CheckedIn);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
