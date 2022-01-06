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
        public virtual DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Stock>().HasData(
                new Stock { Id = 1, Name = "Allegro.eu SA", Value = 10 },
                new Stock { Id = 2, Name = "Grupa Lotos SA", Value = 20 },
                new Stock { Id = 3, Name = "Orange Polska SA", Value = 30 }
            );
        }
    }
}