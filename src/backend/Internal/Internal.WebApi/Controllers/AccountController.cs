using AutoMapper;
using Dapr.Client;
using Internal.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Controllers
{
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private const string StoreName = "statestore";
        private readonly string KeycloakUrl;

        private readonly DaprClient _daprClient;
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public AccountController(DaprClient daprClient, DatabaseContext dbContext, IMapper mapper, IHttpClientFactory factory, IConfiguration configuration)
        {
            KeycloakUrl = configuration["Jwt:Authority"];
            _daprClient = daprClient;
            _dbContext = dbContext;
            _mapper = mapper;
            _httpClient = factory.CreateClient();
        }

        [HttpGet("account")]
        public async Task<ActionResult<AccountDto>> GetAccount()
        {
            var userName = User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Number == userName);
            return account == null ? NotFound() : Ok(_mapper.Map<AccountDto>(account));
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
            if (account == null)
            {
                return NotFound();
            }
            var newTransaction = new Transaction
            {
                Amount = request.Amount,
                Description = request.Description,
                Account = account,
                TransactionType = TransactionType.OutgoingTransfer,
                Date = DateTime.UtcNow,
                Currency = account.Currency
            };
            await _dbContext.Transactions.AddAsync(newTransaction);
            await _dbContext.SaveChangesAsync();

            return Created("", _mapper.Map<TransactionDto>(newTransaction));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Account>> Register(RegisterDto request)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("username", "admin"));
            nvc.Add(new KeyValuePair<string, string>("password", "admin"));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
            nvc.Add(new KeyValuePair<string, string>("client_id", "admin-cli"));

            var req = new HttpRequestMessage(HttpMethod.Post, $"{KeycloakUrl}/protocol/openid-connect/token") { Content = new FormUrlEncodedContent(nvc) };
            var res = await _httpClient.SendAsync(req);
            var content = await res.Content.ReadAsStringAsync();
            var x = JsonConvert.DeserializeObject<KeycloakResponse>(content);

            var req2 = new HttpRequestMessage(HttpMethod.Post, $"{KeycloakUrl.Replace("auth", "auth/admin")}/users");
            req2.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            req2.Headers.Add("Authorization", $"Bearer {x.access_token}");
            var res2 = await _httpClient.SendAsync(req2);

            var account = new Account
            {
            };
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();

            //await _daprClient.PublishEventAsync(StoreName, "user", account);
            return Ok();
        }
    }

    public record KeycloakResponse
    {
        public string access_token { get; set; }
    }
}