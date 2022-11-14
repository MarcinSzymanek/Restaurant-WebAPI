using Microsoft.AspNetCore.Mvc;

namespace HotelRestaurantAPI.Services
{
    public class IRestaurantService : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
