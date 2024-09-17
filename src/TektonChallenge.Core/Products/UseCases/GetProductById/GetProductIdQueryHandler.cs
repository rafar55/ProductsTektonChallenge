using MediatR;
using TektonChallenge.Core.Common.Cache;
using TektonChallenge.Core.Common.Persistence;
using TektonChallenge.Core.Exceptions;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.Repostories;
using TektonChallenge.Core.Products.Services;

namespace TektonChallenge.Core.Products.UseCases.GetProductById;

public class GetProductIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductWithDiscount>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscountService _discountService;
    private readonly ICacheService _cacheService;

    public GetProductIdQueryHandler(
        IUnitOfWork unitOfWork, 
        IDiscountService discountService, 
        ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _discountService = discountService;
        _cacheService = cacheService;
    }

    public async Task<ProductWithDiscount> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IProductRepository>();
        
        var product = await repository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            throw new EntityNotFoundException(request.ProductId.ToString());
        }
        
        //Get or set the discounts for the product in/from cache
        var discounts = await _cacheService.GetOrSet(
            key: $"discount-{product.ProductId.ToString()}",
            onCacheMiss: async () => await _discountService.GetCurrentDiscountsAsync(cancellationToken),
            cancellationToken: cancellationToken);
        
        var currentDiscount = discounts.FirstOrDefault(d => d.ProductId == product.ProductId.ToString());


        return new ProductWithDiscount()
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DiscountPercentage = currentDiscount?.Percentage ?? 0,
            Status = product.Status,
            Stock = product.Stock,
            CreatedAt = product.CreatedAt
        };
    }
}