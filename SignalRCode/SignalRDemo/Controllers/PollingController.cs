using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Models.ViewModels;
using SignalRDemo.Providers.Accounts;

namespace SignalRDemo.Controllers
{
    public class PollingController(IAccountsProvider accountsProvider) : Controller
    {
        private readonly IAccountsProvider accountsProvider = accountsProvider;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAccounts([FromQuery] int userId)
        {
            IEnumerable<Models.Providers.Account> accounts = this.accountsProvider.GetByUser(userId);

            List<AccountViewModel> accountsViewModel = accounts.Select(a => new AccountViewModel
            {
                AccountId = a.Id,
                IBAN = a.IBAN,
                Balance = a.Balance.Amount
            }).ToList();

            return Json(new { Accounts = accountsViewModel });
        }

        [HttpGet]
        public IActionResult GetBalance([FromQuery] int accountId)
        {
            Random random = new();

            int randomNext = random.Next(100, 500);

            bool isBalanceAvailable = randomNext > 400;

            BalancePollingViewModel result = new()
            {
                AccountId = accountId,
                IsBalanceAvailable = isBalanceAvailable,
            };

            if (isBalanceAvailable)
            {
                result.Balance = randomNext;
            }

            return Json(result);
        }
    }
}
