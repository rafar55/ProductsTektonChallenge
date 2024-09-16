using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.UseCases.AddProduct;
using TektonChallenge.Core.Products.UseCases.GetProductById;

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

    [HttpGet("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] Ulid productId)
    {
        var query = new GetProductByIdQuery(productId);
        return await _mediator.Send(query);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] AddProductCommand command)
    {
        var productId = await _mediator.Send(command);
        var createdProduct = await _mediator.Send(new GetProductByIdQuery(productId));
        return CreatedAtAction(
            nameof(GetProduct),
            new { productId = createdProduct.ProductId },
            createdProduct);
    }
}