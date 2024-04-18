using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs.ExampleHubs
{
    public class BasicHub : Hub
    {
        public async Task NewBalanceRecieved(decimal amount) =>
            await Clients.All.SendAsync("BalanceReceived", amount);
    }
}
