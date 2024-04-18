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
                            IBAN = "BG40BUIN95611017084268",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 100
                            }
                        },
                        new()
                        {
                            Id = 2,
                            IBAN = "BG19STSA93001012248583",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 213
                            }
                        },
                        new()
                        {
                            Id = 3,
                            IBAN = "BG94BUIN95611675115097",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 58
                            }
                        }
                    ],
                2 => [
                        new()
                        {
                            Id = 1,
                            IBAN = "BG40BUIN95611017084268",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 100
                            }
                        },
                        new()
                        {
                            Id = 3,
                            IBAN = "BG94BUIN95611675115097",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 58
                            }
                        },
                        new()
                        {
                            Id = 4,
                            IBAN = "BG47UNCR96601128982931",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 77
                            }
                        }
                    ],
                _ => [
                        new()
                        {
                            Id = 1,
                            IBAN = "BG40BUIN95611017084268",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 100
                            }
                        },
                        new()
                        {
                            Id = 2,
                            IBAN = "BG19STSA93001012248583",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 213
                            }
                        },
                        new()
                        {
                            Id = 3,
                            IBAN = "BG94BUIN95611675115097",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 58
                            }
                        },
                        new()
                        {
                            Id = 4,
                            IBAN = "BG47UNCR96601128982931",
                            Balance = new Models.Providers.Balance()
                            {
                                Amount = 77
                            }
                        }
                    ],
            };
        }
    }
}
