namespace TektonChallenge.Api.Contracts.Products;

public record ProductResponse
{
    public Ulid Id { get; init; }
    public string Name { get; init; }
    public string StatusName { get; init; }
    public int Stock { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
}