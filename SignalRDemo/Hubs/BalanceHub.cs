using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Providers.Balance;

namespace SignalRDemo.Hubs
{
    #region Basic Hub

    //public class BalanceHub : Hub
    //{
    //    public async Task NewBalanceRecieved(decimal amount) =>
    //        await Clients.All.SendAsync("BalanceReceived", amount);
    //}

    #endregion


    #region Strongly types Hub

    //public class BalanceHub : Hub<IBalanceHubClient>
    //{
    //    public async Task NewBalanceRecieved(decimal amount, int accountId) =>
    //        await Clients.All.BalanceReceived(amount, accountId);
    //}

    #endregion


    #region Inject services into a hub

    //public class BalanceHub(ILogger<BalanceHub> logger, IBalanceProvider balanceProvider) : Hub<IBalanceHubClient>
    //{
    //    private readonly IBalanceProvider balanceProvider = balanceProvider;
    //    private readonly ILogger<BalanceHub> logger = logger;

    //    public async Task NewBalanceRecieved(decimal amount, int accountId) =>
    //        await Clients.All.BalanceReceived(amount, accountId);
    //}

    #endregion


    #region Connection events

    //public class BalanceHub(ILogger<BalanceHub> logger, IBalanceProvider balanceProvider) : Hub<IBalanceHubClient>
    //{
    //    private readonly IBalanceProvider balanceProvider = balanceProvider;
    //    private readonly ILogger<BalanceHub> logger = logger;

    //    public async Task NewBalanceRecieved(decimal amount, int accountId) =>
    //        await Clients.All.BalanceReceived(amount, accountId);

    //    public override async Task OnConnectedAsync()
    //    {
    //        await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
    //    }

    //    public override Task OnDisconnectedAsync(Exception? exception)
    //    {
    //        if (exception is not null)
    //        {
    //            this.logger.LogError(exception, "Exception occurred and it resulted on client disconnecting");
    //        }

    //        return base.OnDisconnectedAsync(exception);
    //    }
    //}

    #endregion


    #region Instant result
    //public class BalanceHub(IBalanceProvider balanceProvider) : Hub<IBalanceHubClient>
    //{
    //    private readonly IBalanceProvider balanceProvider = balanceProvider;

    //    public async void GetBalance(int accountId)
    //    {
    //        Balance balance = await this.balanceProvider.GetAsync(accountId);

    //        await this.Clients.All.BalanceReceived(balance.Amount, accountId);
    //    }
    //}
    #endregion


    #region Async service
    public class BalanceHub(IBalanceAsyncProvider balanceProvider) : Hub<IBalanceHubClient>
    {
        private readonly IBalanceAsyncProvider balanceProvider = balanceProvider;

        public async Task GetBalance(int accountId)
        {
            await this.Clients.All.StartedLoading(accountId);

            await this.balanceProvider.GetAsync(accountId);
        }
    }
    #endregion
}
