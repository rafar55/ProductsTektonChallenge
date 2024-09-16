using FluentValidation;
using MediatR;
using TektonChallenge.Core.Common.Persistence;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Core.Products.Repostories;

namespace TektonChallenge.Core.Products.UseCases.AddProduct;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Ulid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TimeProvider _timeProvider;
    private readonly IValidator<AddProductCommand> _validator;

    public AddProductCommandHandler(
        IUnitOfWork unitOfWork, 
        TimeProvider timeProvider,
        IValidator<AddProductCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _timeProvider = timeProvider;
        _validator = validator;
    }

    public async Task<Ulid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var repository = _unitOfWork.GetRepository<IProductRepository>();
        var model = new Product()
        {
            ProductId = Ulid.NewUlid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            Status = request.Status,
            CreatedAt = _timeProvider.GetUtcNow()
        };
        await repository.AddAsync(model, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return model.ProductId;
    }
}