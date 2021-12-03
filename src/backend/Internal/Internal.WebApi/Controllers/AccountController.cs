using Dapr.Client;
using Internal.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Internal.Controllers
{
    //[Authorize]
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

        [HttpGet("account")]
        public ActionResult<Account> GetAccount()
        {
            var account = new Account
            {
                Number = 1234,
                Balance = 150190,
                Currency = "PLN",
                OpenedOn = DateTime.Now.AddDays(-5)
            };

            return Ok(account);
        }

        [HttpGet("transactions")]
        public ActionResult<TransactionDetailsListResponseDto> GetTransactions()
        {
            var result = new TransactionDetailsListResponseDto
            {
                Items = new List<Transaction>
                {
                    new Transaction
                    {
                        Id = 1,
                        Ammount = 100,
                        Currency = "PLN",
                        TransactionType = TransactionType.OutgoingTransfer,
                        Date = DateTime.Now.AddDays(-1),
                    },
                    new Transaction
                    {
                        Id = 2,
                        Ammount = 200,
                        Currency = "PLN",
                        TransactionType = TransactionType.IncomingTransfer,
                        Date = DateTime.Now.AddDays(-2),
                    },
                    new Transaction
                    {
                        Id = 3,
                        Ammount = 300,
                        Currency = "PLN",
                        TransactionType = TransactionType.OutgoingTransfer,
                        Date = DateTime.Now.AddDays(-3),
                    },
                }
            };

            return Ok(result);
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