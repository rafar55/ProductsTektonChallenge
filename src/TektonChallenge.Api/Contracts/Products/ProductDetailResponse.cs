namespace TektonChallenge.Api.Contracts.Products;

public class ProductDetailResponse
{
    public Ulid Id { get; init; }
    public required string Name { get; init; }
    public required string StatusName { get; init; }
    public int Stock { get; init; }
    public required string Description { get; init; }
    public decimal Price { get; init; }
    public decimal Discount { get; init; }
    public decimal FinalPrice { get; init; }
}