using SignalRDemo.HostedServices;

namespace SignalRDemo.Providers.Balance
{
    public interface IBalanceAsyncProvider
    {
        Task GetAsync(int accountId);
    }

    public class BalanceAsyncProvider(BalanceBackgroundService balanceBackgroundService) : IBalanceAsyncProvider
    {
        private readonly BalanceBackgroundService balanceBackgroundService = balanceBackgroundService;

        public Task GetAsync(int accountId)
        {
            balanceBackgroundService.GetBalance(accountId);

            return Task.CompletedTask;
        }
    }
}
