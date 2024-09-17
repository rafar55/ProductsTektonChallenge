using FluentValidation;
using MediatR;
using TektonChallenge.Core.Common.Persistence;
using TektonChallenge.Core.Exceptions;
using TektonChallenge.Core.Products.Repostories;

namespace TektonChallenge.Core.Products.UseCases.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateProductCommand> _validator;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateProductCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var repository = _unitOfWork.GetRepository<IProductRepository>();

        var product = await repository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            throw new EntityNotFoundException(request.ProductId.ToString());
        }

        product.Description = request.Description;
        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.Status = request.Status;

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}