namespace SignalRDemo.Models.ViewModels
{
    public class AccountViewModel
    {
        public required int AccountId { get; set; }

        public required string IBAN { get; set; }

        public required decimal Balance { get; set; }
    }
}
