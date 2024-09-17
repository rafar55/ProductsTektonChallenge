using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IEnumerable<Product>> GetProducts(string? search, StatusEnum? status)
    {
        var query = new GetProductsQuery(search, status);
        return await _mediator.Send(query);
    }
    
    [HttpGet("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] Ulid productId)
    {
        var query = new GetProductByIdQuery(productId);
        return await _mediator.Send(query);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] AddProductCommand command)
    {
        var productId = await _mediator.Send(command);
        var createdProduct = await _mediator.Send(new GetProductByIdQuery(productId));
        return CreatedAtAction(
            nameof(GetProduct),
            new { productId = createdProduct.ProductId },
            createdProduct);
    }
    
    [HttpPut("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> UpdateProduct([FromRoute] Ulid productId, [FromBody] UpdateProductCommand command)
    {
        command.ProductId = productId;
        await _mediator.Send(command);
        return await _mediator.Send(new GetProductByIdQuery(productId));
    }
}