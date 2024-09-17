using MediatR;
using TektonChallenge.Core.Common.Persistence;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.Repostories;

namespace TektonChallenge.Core.Products.UseCases.GetProducts;

public class GerProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GerProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IProductRepository>();
        var products = await repository.GetProductsAsync(request, cancellationToken);
        return products;
    }
}