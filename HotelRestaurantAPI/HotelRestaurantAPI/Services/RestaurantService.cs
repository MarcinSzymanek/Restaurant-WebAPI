using Microsoft.AspNetCore.SignalR;

namespace HotelRestaurantAPI.Services
{
    public class RestaurantService : Hub<IRestaurantService>
    {
        public async Task RestaurantUpdate()
        {
            await Clients.All.RestaurantUpdate();
        }
    }
}
