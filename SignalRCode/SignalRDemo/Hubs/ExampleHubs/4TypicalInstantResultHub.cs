using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Models.Providers;
using SignalRDemo.Providers.Balance;

namespace SignalRDemo.Hubs.ExampleHubs
{
    public class TypicalInstantResultHub(IBalanceProvider balanceProvider) : Hub<IBalanceHubClient>
    {
        private readonly IBalanceProvider balanceProvider = balanceProvider;

        public async void GetBalance(int accountId)
        {
            Balance balance = await this.balanceProvider.GetAsync(accountId);

            await this.Clients.All.BalanceReceived(balance.Amount, accountId);
        }
    }
}
