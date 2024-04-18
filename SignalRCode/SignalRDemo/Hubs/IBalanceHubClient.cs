namespace SignalRDemo.Hubs
{
    public interface IBalanceHubClient
    {
        Task BalanceReceived(decimal amount, int accountId);
    }
}
