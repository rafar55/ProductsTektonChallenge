using MediatR;
using TektonChallenge.Core.Products.Models;

namespace TektonChallenge.Core.Products.UseCases.GetProductById;

public record GetProductByIdQuery(Ulid ProductId) : IRequest<ProductWithDiscount>;