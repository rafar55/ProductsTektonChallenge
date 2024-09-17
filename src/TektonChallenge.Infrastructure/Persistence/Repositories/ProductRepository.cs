using Microsoft.EntityFrameworkCore;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.Repostories;
using TektonChallenge.Core.Products.UseCases.GetProducts;

namespace TektonChallenge.Infrastructure.Persistence.Repositories;

internal class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(GetProductsQuery request, CancellationToken ct = default)
    {
        var search = request.Search ?? string.Empty;

        var query = _db.Products
            .Where(p => (p.Name.Contains(search) || p.Description.Contains(search))
                        && (request.Status == null || p.Status == request.Status));

        return await query.ToListAsync(ct);
    }
}