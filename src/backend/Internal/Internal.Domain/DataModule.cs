using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Internal.Domain
{
    public static class DataModule
    {
        public static IServiceCollection AddDataModule(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<DatabaseContext>(options =>
            {
                //options.UseSqlServer(configuration.GetConnectionString("DbConnection"), options => options.EnableRetryOnFailure());
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));

                options.UseSnakeCaseNamingConvention();
            });
            return service;
        }
    }
}