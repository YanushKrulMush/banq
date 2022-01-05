using AutoMapper;
using Broker.Domain;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Broker.Controllers
{
    [Authorize]
    [ApiController]
    public class BrokerController : ControllerBase
    {
        private const string PubSubName = "redis-pubsub";
        private readonly string KeycloakUrl;

        private readonly DaprClient _daprClient;
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public BrokerController(DaprClient daprClient, DatabaseContext dbContext, IMapper mapper, IHttpClientFactory factory, IConfiguration configuration)
        {
            KeycloakUrl = configuration["Jwt:Authority"];
            _daprClient = daprClient;
            _dbContext = dbContext;
            _mapper = mapper;
            _httpClient = factory.CreateClient();
        }

        [HttpGet("stocks")]
        public async Task<ActionResult<AccountDto>> GetStocks()
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var account = await _dbContext.Accounts
                .Include(x => x.AccountStocks)
                    .ThenInclude(x => x.Stock)
                .FirstOrDefaultAsync(x => x.Number == userName);
            return account == null ? NotFound() : Ok(_mapper.Map<List<StockDto>>(account.AccountStocks));
        }

        [HttpPost("stocks/buy")]
        public async Task<ActionResult<int>> BuyStock(StockOperationDto request)
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(x => x.Number == userName);
            var accountStock = await _dbContext.AccountStocks
                .FirstOrDefaultAsync(x => x.AccountId == account.Id && x.StockId == request.StockId);
            if (accountStock is null)
            {
                accountStock = new AccountStock { AccountId = account.Id, Quantity = request.Quantity, StockId = request.StockId };
                await _dbContext.AccountStocks.AddAsync(accountStock);
            }
            else
            {
                accountStock.Quantity += request.Quantity;
            }
            await _dbContext.SaveChangesAsync();
            return Ok(accountStock.Quantity);
        }

        [HttpPost("stocks/sell")]
        public async Task<ActionResult<int>> GetAccount(StockOperationDto request)
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(x => x.Number == userName);
            var accountStock = await _dbContext.AccountStocks
                .FirstOrDefaultAsync(x => x.AccountId == account.Id && x.StockId == request.StockId);
            if (accountStock is null)
            {
                return BadRequest($"Account have no stocks with id {request.StockId}");
            }
            else
            {
                if (request.Quantity > accountStock.Quantity)
                {
                    return BadRequest($"Do not have enough stocks to sell");
                }
                accountStock.Quantity -= request.Quantity;
            }
            await _dbContext.SaveChangesAsync();
            return Ok(accountStock.Quantity);
        }

        [Topic(PubSubName, "user")]
        [HttpPost("add/user")]
        public async Task<ActionResult<Account>> AddUser(AccountDto account)
        {
            var newAccount = new Account
            {
                Number = account.Number,
            };
            await _dbContext.Accounts.AddAsync(newAccount);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }

    public record KeycloakResponse
    {
        public string access_token { get; set; }
    }
}