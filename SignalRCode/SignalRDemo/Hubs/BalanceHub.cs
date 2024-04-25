using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Providers.Balance;

namespace SignalRDemo.Hubs
{
    public class BalanceHub(IBalanceAsyncProvider balanceAsyncProvider) : Hub<IBalanceHubClient>
    {
        private readonly IBalanceAsyncProvider balanceAsyncProvider = balanceAsyncProvider;

        //Client sends a request and does not expect a response right away
        public async Task GetBalance(int accountId)
        {
            await this.Clients.All.StartedLoading(accountId);

            await this.balanceAsyncProvider.GetAsync(accountId);
        }
    }
}
