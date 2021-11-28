using Dapr;
using Dapr.Client;
using Internal.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Internal.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private const string StoreName = "statestore";

        private readonly DaprClient _daprClient;
        private readonly ILogger<AccountController> _logger;

        public AccountController(DaprClient daprClient,
            ILogger<AccountController> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        [HttpGet("test")]
        public ActionResult Get()
        {
            return Ok("dupa");
        }


        //[HttpGet("{account}")]
        //public ActionResult<Account> Get([FromState(StoreName)] StateEntry<Account> account)
        //{
        //    if (account.Value is null)
        //    {
        //        return this.NotFound();
        //    }

        //    return account.Value;
        //}


        //[Topic("pubsub", "deposit")]
        //[HttpPost("deposit")]
        //public async Task<ActionResult<Account>> Deposit(Transaction transaction)
        //{
        //    _logger.LogDebug("Enter deposit");
        //    var state = await _daprClient.GetStateEntryAsync<Account>(StoreName, transaction.Id);
        //    state.Value ??= new Account() { Id = transaction.Id, };
        //    state.Value.Balance += transaction.Amount;
        //    await state.SaveAsync();
        //    return state.Value;
        //}

        //[Topic("pubsub", "withdraw")]
        //[HttpPost("withdraw")]
        //public async Task<ActionResult<Account>> Withdraw(Transaction transaction)
        //{
        //    _logger.LogDebug("Enter withdraw");
        //    var state = await _daprClient.GetStateEntryAsync<Account>(StoreName, transaction.Id);

        //    if (state.Value == null)
        //    {
        //        return NotFound();
        //    }

        //    state.Value.Balance -= transaction.Amount;
        //    await state.SaveAsync();
        //    return state.Value;
        //}
    }
}