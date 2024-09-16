using MediatR;
using TektonChallenge.Core.Products.Models;

namespace TektonChallenge.Core.Products.UseCases.AddProduct;

public record AddProductCommand : IRequest<Ulid>
{
    public required string Name { get; set; }
    public required StatusEnum Status { get; set; }
    public required int Stock { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}