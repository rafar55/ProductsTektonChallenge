using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.UseCases.AddProduct;
using TektonChallenge.Core.Products.UseCases.GetProductById;
using TektonChallenge.Core.Products.UseCases.UpdateProduct;

namespace TektonChallenge.Api.Contracts.Products;

public static class ProductMapper
{
    public static AddProductCommand ToAddCommand(this ProductRequest dto)
    {
        return new AddProductCommand
        {
            Name = dto.Name,
            Status = dto.Status,
            Stock = dto.Stock,
            Description = dto.Description,
            Price = dto.Price
        };
    }
    
    public static UpdateProductCommand ToUpdateCommand(this ProductRequest dto, Ulid id)
    {
        return new UpdateProductCommand
        {
            ProductId = id,
            Name = dto.Name,
            Status = dto.Status,
            Stock = dto.Stock,
            Description = dto.Description,
            Price = dto.Price
        };
    }
    
    public static ProductListResponse ToResponse(this Product model)
    {
        return new ProductListResponse()
        {
            Id = model.ProductId,
            Name = model.Name,
            StatusName = model.Status.ToString(),
            Stock = model.Stock,
            Description = model.Description,
            Price = model.Price
        };
    }
    
    public static ProductDetailResponse ToResponse(this ProductWithDiscount model)
    {
        return new ProductDetailResponse()
        {
            Id = model.ProductId,
            Name = model.Name,
            StatusName = model.Status.ToString(),
            Stock = model.Stock,
            Description = model.Description,
            Price = model.Price,
            Discount = model.DiscountPercentage ?? 0,
            FinalPrice = model.PriceWithDiscount
        };
    }
    
    public static IEnumerable<ProductListResponse> ToResponse(this IEnumerable<Product> models)
    {
        return models.Select(ToResponse);
    }
}