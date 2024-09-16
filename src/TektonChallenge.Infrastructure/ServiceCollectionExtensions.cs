using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TektonChallenge.Core.Products.Repostories;
using TektonChallenge.Infrastructure.Data;
using TektonChallenge.Infrastructure.Data.Repositories;

namespace TektonChallenge.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(connectionString); },
            contextLifetime: ServiceLifetime.Scoped);

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}