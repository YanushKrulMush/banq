using Broker.Domain;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Broker.WebApi.Controllers
{
    [ApiController]
    public class PubSub : Controller
    {
        private const string PubSubName = "redis-pubsub";
        private readonly DatabaseContext _dbContext;

        public PubSub(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Topic(PubSubName, "user")]
        [HttpPost("add/user")]
        public async Task<ActionResult<Account>> AddUser(AccountDto account)
        {
            System.Console.WriteLine("YES");
            var newAccount = new Account
            {
                Number = account.Number,
            };
            await _dbContext.Accounts.AddAsync(newAccount);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
