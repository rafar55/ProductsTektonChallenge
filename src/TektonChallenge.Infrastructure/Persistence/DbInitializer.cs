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
                ProductId = Ulid.Parse("01J7YZA8V91W66YW8NSP7GPNM3"),
                Name = "Product 1",
                Status = StatusEnum.Active,
                Description = "Product with 10% off",
                Price = 10.0m,
                Stock = 10
            },
            new Product
            {
                ProductId = Ulid.Parse("01J7YZA8V9EYRBBV514300VH6X"),
                Name = "Product 2",
                Status = StatusEnum.Active,
                Description = "Product with 5% off",
                Price = 20.0m,
                Stock = 20
            },
            new Product
            {
                ProductId = Ulid.Parse("01J7YZA8V958BMGW68TWZ8X98V"),
                Name = "Product 3",
                Status = StatusEnum.Active,
                Description = "Product with 20% off",
                Price = 30.0m,
                Stock = 30
            },
            new Product
            {
                ProductId = Ulid.Parse("01J7YZA8V90Z66BJDJ9QQ65S0M"),
                Name = "Product 4",
                Status = StatusEnum.Active,
                Description = "Product with 17.5% off",
                Price = 30.0m,
                Stock = 30
            }
        };

        await context.AddRangeAsync(products);
    }
}