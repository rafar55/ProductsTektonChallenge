using MediatR;
using TektonChallenge.Core.Products.Models;

namespace TektonChallenge.Core.Products.UseCases.GetProducts;

public record GetProductsQuery(string? Search, StatusEnum? Status) : IRequest<IEnumerable<Product>>;