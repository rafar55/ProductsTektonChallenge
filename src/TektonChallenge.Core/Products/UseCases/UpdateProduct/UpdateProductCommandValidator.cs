using FluentValidation;

namespace TektonChallenge.Core.Products.UseCases.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
        
        RuleFor(x => x.Status)
            .IsInEnum();
        
        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(x => x.Stock)
            .NotEmpty()
            .GreaterThan(0);
    }
}