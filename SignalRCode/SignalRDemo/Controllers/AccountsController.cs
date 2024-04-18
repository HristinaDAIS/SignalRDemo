using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Models.ViewModels;
using SignalRDemo.Providers.Accounts;

namespace SignalRDemo.Controllers
{
    public class AccountsController(IAccountsProvider accountsProvider) : Controller
    {
        private readonly IAccountsProvider accountsProvider = accountsProvider;

        public IActionResult Index([FromQuery] int userId)
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
    }
}
