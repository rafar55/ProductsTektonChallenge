using MediatR;
using TektonChallenge.Core.Common.Data;
using TektonChallenge.Core.Products.Repostories;

namespace TektonChallenge.Core.Products.UseCases.AddProduct;

public class AddProductCommandHandler  : IRequestHandler<AddProductCommand, Ulid>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<Ulid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IProductRepository>();
        throw new NotImplementedException();
    }
}