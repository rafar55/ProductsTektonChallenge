using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TektonChallenge.Api.Contracts.Products;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.UseCases.AddProduct;
using TektonChallenge.Core.Products.UseCases.GetProductById;
using TektonChallenge.Core.Products.UseCases.GetProducts;
using TektonChallenge.Core.Products.UseCases.UpdateProduct;

namespace TektonChallenge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<ProductListResponse>> GetProducts(string? search, StatusEnum? status)
    {
        var query = new GetProductsQuery(search, status);
        var products = await _mediator.Send(query);
        return products.ToResponse();
    }
    
    [HttpGet("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDetailResponse>> GetProduct([FromRoute] Ulid productId)
    {
        var query = new GetProductByIdQuery(productId);
        var product = await _mediator.Send(query);
        return product.ToResponse();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDetailResponse>> CreateProduct([FromBody] ProductRequest request)
    {
        var productId = await _mediator.Send(request.ToAddCommand());
        var createdProduct = await _mediator.Send(new GetProductByIdQuery(productId));
        return CreatedAtAction(
            nameof(GetProduct),
            new { productId = createdProduct.ProductId },
            createdProduct.ToResponse());
    }
    
    [HttpPut("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDetailResponse>> UpdateProduct([FromRoute] Ulid productId, [FromBody] ProductRequest request)
    {
        var updateCommand = request.ToUpdateCommand(productId);
        await _mediator.Send(updateCommand);
        var updatedProduct = await _mediator.Send(new GetProductByIdQuery(productId));
        return updatedProduct.ToResponse();
    }
}