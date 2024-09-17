using MediatR;
using TektonChallenge.Core.Products.Models;

namespace TektonChallenge.Core.Products.UseCases.UpdateProduct;

public record UpdateProductCommand : IRequest
{
    public required Ulid ProductId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required StatusEnum Status { get; init; }
    public required decimal Price { get; init; }
    public required int Stock { get; init; }
}