using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TektonChallenge.Core.Products.Models;
using Microsoft.Extensions.Hosting;

namespace TektonChallenge.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedAndRunMigrationsAsync(IHost app)
    {
        using var scopes = app.Services.CreateScope();
        var services = scopes.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();

        //Run Ef Migrations
        await context.Database.MigrateAsync();

        //Seed Data
        await SeedProductsAsync(context);

        await context.SaveChangesAsync();
    }

    private static async Task SeedProductsAsync(AppDbContext context)
    {
        if (await context.Products.AnyAsync())
        {
            return;
        }

        var products = new List<Product>()
        {
            new()
            {
                ProductId = Ulid.NewUlid(),
                Name = "Product 1",
                Status = StatusEnum.Active,
                Description = "Description 1",
                Price = 10.0m,
                Stock = 10
            },
            new Product
            {
                ProductId = Ulid.NewUlid(),
                Name = "Product 2",
                Status = StatusEnum.Active,
                Description = "Description 2",
                Price = 20.0m,
                Stock = 20
            },
            new Product
            {
                ProductId = Ulid.NewUlid(),
                Name = "Product 3",
                Status = StatusEnum.Active,
                Description = "Description 3",
                Price = 30.0m,
                Stock = 30
            }
        };

        await context.AddRangeAsync(products);
    }
}