using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Providers.Balance;

namespace SignalRDemo.Hubs.ExampleHubs
{
    public class ConnectionEventsHub(ILogger<ConnectionEventsHub> logger) : Hub<IBalanceHubClient>
    {
        private readonly ILogger<ConnectionEventsHub> logger = logger;

        public async Task NewBalanceRecieved(decimal amount, int accountId) =>
            await Clients.All.BalanceReceived(amount, accountId);

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception is not null)
            {
                this.logger.LogError(exception, "Exception occurred and it resulted on client disconnecting");
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
