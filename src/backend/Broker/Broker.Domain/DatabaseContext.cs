using Microsoft.EntityFrameworkCore;

namespace Broker.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountStock> AccountStocks { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}