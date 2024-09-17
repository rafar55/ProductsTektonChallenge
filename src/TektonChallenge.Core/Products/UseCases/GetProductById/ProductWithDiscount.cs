using TektonChallenge.Core.Products.Models;

namespace TektonChallenge.Core.Products.UseCases.GetProductById;

public record ProductWithDiscount
{
    public required Ulid ProductId { get; init; }
    public required string Name { get; init; }
    public required StatusEnum Status { get; init; }
    public required int Stock { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public required decimal? DiscountPercentage { get; init; }
    public decimal DiscountAmount => Price * (DiscountPercentage ?? 0);
    public decimal PriceWithDiscount => Price - DiscountAmount;
}