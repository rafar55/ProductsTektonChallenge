namespace TektonChallenge.Core.Products.Models;

public class Product
{
    public required Ulid ProductId { get; set; }
    public required string Name { get; set; }
    public required StatusEnum Status { get; set; }
    public required int Stock { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
}