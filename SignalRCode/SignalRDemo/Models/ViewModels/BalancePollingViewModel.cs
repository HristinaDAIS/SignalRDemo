namespace SignalRDemo.Models.ViewModels
{
    public class BalancePollingViewModel
    {
        public int AccountId { get; set; }

        public bool IsBalanceAvailable { get; set; }

        public decimal?  Balance { get; set; }
    }
}
