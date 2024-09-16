using Microsoft.EntityFrameworkCore;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Infrastructure.Data.Repositories;

namespace TektonChallenge.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        
        //Enum general convention store as string
        configurationBuilder
            .Properties<Enum>()
            .HaveConversion<string>()
            .HaveMaxLength(200);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseRepository<>).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}