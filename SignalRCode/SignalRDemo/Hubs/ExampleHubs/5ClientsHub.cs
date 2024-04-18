using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs.ExampleHubs
{
    public class ClientsHub : Hub<IBalanceHubClient>
    {
        //Sends a message to all connected clients
        public async Task SendToAll(decimal amount, int accountId) =>
            await Clients.All.BalanceReceived(amount, accountId);

        //Sends a message to a group of clients
        public async Task SendToGroup(decimal amount, int accountId) =>
          await Clients.Groups("GroupName").BalanceReceived(amount, accountId);

        //Sends a message to the client that invoked the method on the server
        public async Task SendToCaller(decimal amount, int accountId) =>
          await Clients.Caller.BalanceReceived(amount, accountId);

        //Sends a message to a certain client that is identified by thir unique connectionId
        //To do that you need to store the connectionId in your application when the client connects to the Hub
        public async Task SendToCertainClient(decimal amount, int accountId) =>
          await Clients.Client("connectionId").BalanceReceived(amount, accountId);
    }
}
