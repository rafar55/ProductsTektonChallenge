using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TektonChallenge.Core.Products.UseCases.AddProduct;

namespace TektonChallenge.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<AddProductCommand>();
        });

        services.AddValidatorsFromAssemblyContaining<AddProductCommandValidator>();
        
        services.AddSingleton<TimeProvider>(TimeProvider.System);

        return services;
    }
}