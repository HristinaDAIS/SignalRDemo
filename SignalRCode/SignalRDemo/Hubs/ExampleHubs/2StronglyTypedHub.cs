using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs.ExampleHubs
{
    public class StronglyTypedHub : Hub<IBalanceHubClient>
    {
        public async Task NewBalanceRecieved(decimal amount, int accountId) =>
            await Clients.All.BalanceReceived(amount, accountId);
    }
}
