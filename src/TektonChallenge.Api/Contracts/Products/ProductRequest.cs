namespace TektonChallenge.Api.Contracts.Products;

public record ProductRequest
{
    public required string Name { get; set; }
    public required string Status { get; set; }
    public required int Stock { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}