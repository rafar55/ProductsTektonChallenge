using MediatR;

namespace TektonChallenge.Core.Products.UseCases.AddProduct;

public class AddProductCommandHandler  : IRequestHandler<AddProductCommand, Ulid>
{
    public AddProductCommandHandler()
    {
    }
    
    public Task<Ulid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}