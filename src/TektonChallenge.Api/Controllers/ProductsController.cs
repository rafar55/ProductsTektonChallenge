using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TektonChallenge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
}