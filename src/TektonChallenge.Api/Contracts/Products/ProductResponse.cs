namespace TektonChallenge.Api.Contracts.Products;

public record ProductResponse
{
    public Ulid Id { get; init; }
    public required string Name { get; init; }
    public required string StatusName { get; init; }
    public int Stock { get; init; }
    public required string Description { get; init; }
    public decimal Price { get; init; }
}