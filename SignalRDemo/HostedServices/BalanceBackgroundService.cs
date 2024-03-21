
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Hubs;
using SignalRDemo.Providers;
using System.Collections.Concurrent;

namespace SignalRDemo.HostedServices
{
    public class BalanceBackgroundService(IHubContext<BalanceHub, IBalanceHubClient> hubContext) : BackgroundService
    {
        private readonly IHubContext<BalanceHub, IBalanceHubClient> hubContext = hubContext;
        private static readonly ConcurrentQueue<int> queue = new();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                if (queue.TryDequeue(out int accountId))
                {
                    decimal amount = 100 + DateTime.Now.Second;

                    await this.hubContext.Clients.All.BalanceReceived(amount, accountId);
                }
            }
        }

        public void GetBalance(int accountId)
        {
            queue.Enqueue(accountId);
        }
    }
}
