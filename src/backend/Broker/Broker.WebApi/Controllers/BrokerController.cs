using AutoMapper;
using Broker.Domain;
using Dapr.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Broker.Controllers
{
    [Authorize]
    [ApiController]
    public class BrokerController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public BrokerController(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("stocks")]
        public async Task<ActionResult<StockListDto>> GetStocks()
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var account = await _dbContext.Accounts
                .Include(x => x.AccountStocks)
                    .ThenInclude(x => x.Stock)
                .FirstOrDefaultAsync(x => x.Number == userName);
            return account == null
                ? NotFound("Niewłaściwe konto")
                : Ok(new StockListDto { Items = _mapper.Map<List<StockDto>>(account.AccountStocks) });
        }

        [HttpPost("stocks/buy")]
        public async Task<ActionResult<int>> BuyStock(StockOperationDto request)
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(x => x.Number == userName);
            if (account == null)
            {
                return NotFound("Niewłaściwe konto");
            }
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == request.StockId);
            if (stock is null)
            {
                return BadRequest($"Podana akcja nie istnieje w systemie");
            }
            var daprClient = new DaprClientBuilder().Build();
            try
            {
                await daprClient.InvokeMethodAsync<ModifyAccountDto>("dotnet-app", "account", new ModifyAccountDto
                {
                    AccountNumber = account.Number,
                    Amount = stock.Value * request.Quantity
                });
            }
            catch (InvocationException ex)
            {
                return BadRequest(await ex.Response.Content.ReadAsStringAsync());
            }
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
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == request.StockId);
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(x => x.Number == userName);
            var accountStock = await _dbContext.AccountStocks
                .FirstOrDefaultAsync(x => x.AccountId == account.Id && x.StockId == request.StockId);
            var daprClient = new DaprClientBuilder().Build();
            try
            {
                await daprClient.InvokeMethodAsync<ModifyAccountDto>("dotnet-app", "account", new ModifyAccountDto
                {
                    AccountNumber = account.Number,
                    Amount = -stock.Value * request.Quantity
                });
            }
            catch (InvocationException ex)
            {
                return BadRequest(await ex.Response.Content.ReadAsStringAsync());
            }

            if (accountStock is null)
            {
                return BadRequest($"Brak akcji na koncie");
            }
            else
            {
                if (request.Quantity > accountStock.Quantity)
                {
                    return BadRequest($"Niewystarczająca liczba akcji do sprzedania");
                }
                accountStock.Quantity -= request.Quantity;
            }
            await _dbContext.SaveChangesAsync();
            return Ok(accountStock.Quantity);
        }
    }

    public record AccountInfo
    {
        public int Balance { get; set; }
    }

    public record ModifyStockEvent
    {
        public int AccountId { get; set; }

        public int StockId { get; set; }

        public double StockValue { get; set; }

        public int Quantity { get; set; }
    }

    public record ModifyAccountDto
    {
        public string AccountNumber { get; set; }
        public double Amount { get; set; }
    }
}