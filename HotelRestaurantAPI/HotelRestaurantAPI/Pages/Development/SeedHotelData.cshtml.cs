using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;

namespace HotelRestaurantAPI.Pages.Development
{
    public class SeedHotelDataModel : PageModel
    {
        private readonly HotelRestaurantAPI.Data.HotelDataContext _context;

        public SeedHotelDataModel(HotelRestaurantAPI.Data.HotelDataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Guest Guest { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Guests.Add(Guest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
