using SignalRDemo.Models.Providers;

namespace SignalRDemo.Providers.Accounts
{
    public interface IAccountsProvider
    {
        IEnumerable<Account> GetByUser(int userId);
    }

    //in real app -> get from real service or database
    public class AccountsProvider : IAccountsProvider
    {
        public IEnumerable<Account> GetByUser(int userId)
        {
            return userId switch
            {
                1 => [
                        new()
                        {
                            Id = 1,
                            IBAN = "1585 7455 0852 4578",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 100
                            },
                            ValidDate = "05/26"
                        },
                        new()
                        {
                            Id = 2,
                            IBAN = "2455 0852 4578 1585",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 213
                            },
                            ValidDate = "08/28"
                        },
                        new()
                        {
                            Id = 3,
                            IBAN = "4290 3743 8724 8657",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 58
                            },
                            ValidDate = "05/27"
                        }
                    ],
                2 => [
                        new()
                        {
                            Id = 1,
                            IBAN = "1585 7455 0852 4578",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 100
                            },
                            ValidDate = "05/26"
                        },
                        new()
                        {
                            Id = 3,
                            IBAN = "4290 3743 8724 8657",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 58
                            },
                            ValidDate = "05/27"
                        },
                        new()
                        {
                            Id = 4,
                            IBAN = "5548 4540 3846 0736",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 77
                            },
                            ValidDate = "04/26"
                        }
                    ],
                _ => [
                        new()
                        {
                            Id = 1,
                            IBAN = "1585 7455 0852 4578",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 100
                            },
                            ValidDate = "05/26"
                        },
                        new()
                        {
                            Id = 2,
                            IBAN = "2455 0852 4578 1585",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 213
                            },
                            ValidDate = "08/28"
                        },
                        new()
                        {
                            Id = 3,
                            IBAN = "4290 3743 8724 8657",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 58
                            },
                            ValidDate = "05/27"
                        },
                        new()
                        {
                            Id = 4,
                            IBAN = "5548 4540 3846 0736",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 77
                            },
                            ValidDate = "04/26"
                        }
                    ],
            };
        }
    }
}
