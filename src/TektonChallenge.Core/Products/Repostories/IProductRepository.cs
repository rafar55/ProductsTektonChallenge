using TektonChallenge.Core.Common.Persistence;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.UseCases.GetProducts;

namespace TektonChallenge.Core.Products.Repostories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsAsync(GetProductsQuery request, CancellationToken ct = default);
}