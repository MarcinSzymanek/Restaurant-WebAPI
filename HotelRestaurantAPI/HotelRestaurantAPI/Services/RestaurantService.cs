using Microsoft.AspNetCore.Mvc;

namespace HotelRestaurantAPI.Services
{
    public class RestaurantService : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
