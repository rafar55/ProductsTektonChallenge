using MediatR;

namespace TektonChallenge.Core.Products.UseCases.AddProduct;

public record AddProductCommand(string Name, decimal Price, int StatusId) : IRequest<Ulid>;