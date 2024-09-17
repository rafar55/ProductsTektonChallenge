using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TektonChallenge.Core.Common.Cache;
using TektonChallenge.Core.Common.Persistence;
using TektonChallenge.Core.Products.Repostories;
using TektonChallenge.Core.Products.Services;
using TektonChallenge.Infrastructure.Cache;
using TektonChallenge.Infrastructure.Persistence;
using TektonChallenge.Infrastructure.Persistence.Repositories;
using TektonChallenge.Infrastructure.Services;

namespace TektonChallenge.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        string connectionString,
        string discountServiceBaseUrl)
    {
        services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(connectionString); },
            contextLifetime: ServiceLifetime.Scoped);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddHttpClient<IDiscountService, DiscountService>(options =>
        {
            options.BaseAddress = new Uri(discountServiceBaseUrl);
        });

        services.AddDistributedMemoryCache();
        services.AddScoped<ICacheService, CacheService>();

        return services;
    }
}