using Dapr.Client;
using Internal.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Internal.Controllers
{
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private const string StoreName = "statestore";
        private readonly DaprClient _daprClient;
        private readonly ILogger<AccountController> _logger;
        private readonly DatabaseContext _dbContext;

        public AccountController(DaprClient daprClient, ILogger<AccountController> logger, DatabaseContext dbContext)
        {
            _daprClient = daprClient;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("test")]
        public ActionResult Get()
        {
            return Ok("dupa");
        }

        [HttpGet("account")]
        public async Task<ActionResult<Account>> GetAccount()
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Number == userName);
            return account == null ? NotFound() : Ok(account);
        }

        [HttpGet("transactions")]
        public async Task<ActionResult<TransactionDetailsListResponseDto>> GetTransactions()
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var transactions = await _dbContext.Transactions
                .Include(x => x.Account)
                .Where(x => x.Account.Number == userName)
                .ToListAsync();

            return Ok(new TransactionDetailsListResponseDto { Items = transactions });
        }

        [HttpPost("transactions")]
        public async Task<ActionResult> AddTransaction(AddTransactionDto request)
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Number == userName);
            var newTransaction = new Transaction
            {
                Amount = request.Amount,
                Description = request.Description,
                Account = account,
                TransactionType = TransactionType.OutgoingTransfer,
                Date = DateTime.UtcNow
            };
            await _dbContext.Transactions.AddAsync(newTransaction);

            return Created("", new { });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Account>> Register()
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Number == userName);
            return Ok();
        }

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