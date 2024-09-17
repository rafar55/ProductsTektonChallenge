using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.UseCases.AddProduct;
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
    
    public static ProductResponse ToResponse(this Product model)
    {
        return new ProductResponse
        {
            Id = model.ProductId,
            Name = model.Name,
            StatusName = model.Status.ToString(),
            Stock = model.Stock,
            Description = model.Description,
            Price = model.Price
        };
    }
    
    public static IEnumerable<ProductResponse> ToResponse(this IEnumerable<Product> models)
    {
        return models.Select(ToResponse);
    }
}