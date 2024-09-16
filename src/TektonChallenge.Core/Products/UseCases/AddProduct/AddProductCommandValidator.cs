using FluentValidation;
using TektonChallenge.Core.Products.Models;

namespace TektonChallenge.Core.Products.UseCases.AddProduct;

public class AddProductCommandValidator : AbstractValidator<Product>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Stock)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}