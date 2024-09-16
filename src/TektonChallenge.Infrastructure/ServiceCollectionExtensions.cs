using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TektonChallenge.Core.Common.Persistence;
using TektonChallenge.Core.Products.Repostories;
using TektonChallenge.Infrastructure.Persistence;
using TektonChallenge.Infrastructure.Persistence.Repositories;

namespace TektonChallenge.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(connectionString); },
            contextLifetime: ServiceLifetime.Scoped);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}