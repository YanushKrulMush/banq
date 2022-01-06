using AutoMapper;
using Broker.Domain;
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
            if (account == null)
            {
                return NotFound();
            }    
            if (!await _dbContext.Stocks.AnyAsync(x => x.Id == request.StockId))
            {
                return BadRequest($"Stock with id {request.StockId} does not exist");
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
    }
}