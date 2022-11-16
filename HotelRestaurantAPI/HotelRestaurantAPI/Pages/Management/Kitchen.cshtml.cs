using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;
using HotelRestaurantAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;


namespace HotelRestaurantAPI.Pages.Management
{
    public class KitchenModel : PageModel
    {
        private DateTime _now = DateTime.Now;
        private int _day = DateTime.Now.Day;
        private int _month = DateTime.Now.Month;

     
        private readonly HotelRestaurantAPI.Data.HotelDataContext _context;

        public int _adultsExpected;
        public int _childrenExpected;
        public int _totalExpected;
        public int _adultsCheckedIn;
        public int _childrenCheckedIn;

        private readonly IHubContext<KitchenService, IKitchenService> _kitchenContext;
        public KitchenModel(HotelRestaurantAPI.Data.HotelDataContext context,
            IHubContext<KitchenService, IKitchenService> kitchenContext)
        {
            _context = context;
            _kitchenContext = kitchenContext;
        }

        private DateTime _today = DateTime.Now;
        public async Task OnGet()
        {
            
            var myExpected = await GetExpected(_today);
            
            if (myExpected != null)
            {
                _adultsExpected = myExpected.Adults;
                _childrenExpected = myExpected.Children;
                _totalExpected = _adultsExpected + _childrenExpected;
            }
            else
            {
                ModelState.AddModelError("Input.Date", "No quest on this date");
            }

            var myDailyBreakfast = await GetDailyBreakfast(DateTime.Now);
            if (myDailyBreakfast != null)
            {
                foreach (CheckedIn checkedIn in myDailyBreakfast.CheckedIn)
                {
                    _adultsCheckedIn += checkedIn.Adults;
                    _childrenCheckedIn += checkedIn.Children;
                }
            }
            else
            {
                ModelState.AddModelError("Input.Date", "No guest is checked in on this date");
            }
        }

        public async Task OnPost()
        {
            var myExpected = await GetExpected(Input.Date);
            if (myExpected != null)
            {
                _adultsExpected = myExpected.Adults;
                _childrenExpected = myExpected.Children;
                _totalExpected = _adultsExpected + _childrenExpected;
            }
            else
            {
                ModelState.AddModelError("Input.Date", "No quest on this date");
            }

            var myDailyBreakfast = await GetDailyBreakfast(DateTime.Now);
            if (myDailyBreakfast != null)
            {
                foreach (CheckedIn checkedIn in myDailyBreakfast.CheckedIn)
                {
                    _adultsCheckedIn += checkedIn.Adults;
                    _childrenCheckedIn += checkedIn.Children;
                }
            }
            else
            {
                ModelState.AddModelError("Input.Date", "No guest is checked in on this date");
            }
        }

        private async Task<Expected?> GetExpected(DateTime date)
        {
            var breakfasts = await _context.DailyBreakfasts
                .Include(b => b.Expected)
                .Where(p => p.Day == date.Day)
                .FirstOrDefaultAsync();
            return breakfasts.Expected;
        }

        private async Task<DailyBreakfast?> GetDailyBreakfast(DateTime date)
        {
            return await _context.DailyBreakfasts
                .Where(p => p.Day == date.Day)
                .Include(x => x.CheckedIn)
                .FirstOrDefaultAsync();
        }

        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required] 
            [DataType(DataType.Date)] 
            public DateTime Date { get; set; } = DateTime.Now;
        }

    }
}
