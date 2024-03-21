using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Models.ViewModels;
using SignalRDemo.Providers.Accounts;
using System.Diagnostics;

namespace SignalRDemo.Controllers
{
    public class HomeController(IAccountsProvider accountsProvider) : Controller
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
