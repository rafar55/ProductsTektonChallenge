using MediatR;
using TektonChallenge.Core.Common.Persistence;
using TektonChallenge.Core.Exceptions;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.Repostories;

namespace TektonChallenge.Core.Products.UseCases.GetProductById;

public class GetProductIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IProductRepository>();
        
        var product = await repository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            throw new EntityNotFoundException(request.ProductId.ToString());
        }

        return product;
    }
}