namespace SignalRDemo.Models.Providers
{
    public class Account
    {
        public required int Id { get; set; }

        public required string IBAN { get; set; }

        public required Balance Balance { get; set; }
    }
}
