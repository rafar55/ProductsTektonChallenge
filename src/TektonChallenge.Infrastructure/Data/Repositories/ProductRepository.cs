using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.Repostories;

namespace TektonChallenge.Infrastructure.Data.Repositories;

internal class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}