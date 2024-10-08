using FluentValidation;
using TektonChallenge.Core.Products.Models;

namespace TektonChallenge.Core.Products.UseCases.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
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

        RuleFor(x => x.Status)
            .NotEmpty()
            .IsEnumName(typeof(StatusEnum), caseSensitive: false);
    }
}