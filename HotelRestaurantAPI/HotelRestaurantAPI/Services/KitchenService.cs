using HotelRestaurantAPI.Services;
using Microsoft.AspNetCore.SignalR;

namespace HotelRestaurantAPI.Services
{
    public class KitchenService : Hub<IKitchenService>
    {
        public async Task KitchenUpdate()
        {
            await Clients.All.KitchenUpdate();
        }
    }
}
