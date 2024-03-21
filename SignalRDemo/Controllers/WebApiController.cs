using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Hubs;
using SignalRDemo.Models.WebApi;

namespace SignalRDemo.Controllers
{
    [ApiController]
    public class WebApiController(IHubContext<BalanceHub, IBalanceHubClient> hubContext) : Controller
    {
        private readonly IHubContext<BalanceHub, IBalanceHubClient> hubContext = hubContext;

        [HttpPost]
        [Route("/api/balance")]
        public async Task PublishBalance([FromBody] BalanceRequest request)
        {
            await this.hubContext.Clients.All.BalanceReceived(request.Amount, request.AccountId);
        }
    }
}
