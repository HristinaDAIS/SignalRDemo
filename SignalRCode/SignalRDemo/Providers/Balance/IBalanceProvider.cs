namespace SignalRDemo.Providers.Balance
{
    public interface IBalanceProvider
    {
        Task<Models.Providers.Balance> GetAsync(int accountId);
    }

    public class BalanceProvider : IBalanceProvider
    {
        public Task<Models.Providers.Balance> GetAsync(int accountId)
        {
            return Task.FromResult(new Models.Providers.Balance
            {
                Amount = 100 + DateTime.Now.Second
            });
        }
    }
}
