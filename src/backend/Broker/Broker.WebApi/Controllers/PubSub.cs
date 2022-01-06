using Broker.Domain;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Broker.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public class PubSubController : Controller
    {       
        private readonly DatabaseContext _dbContext;

        public PubSubController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Topic(Consts.PubSubName, "user")]
        [HttpPost("add/account")]
        public async Task<ActionResult<Account>> AddUser(AccountDto account)
        {
            System.Console.WriteLine("PUBSUBUJE SIE");
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
