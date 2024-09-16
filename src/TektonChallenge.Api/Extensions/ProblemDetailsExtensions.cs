using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using TektonChallenge.Core.Exceptions;
using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;


namespace TektonChallenge.Api.Extensions;

public static class ProblemDetailExtensions
{
    public static void MapFluentValidation(this ProblemDetailsOptions options)
    {
        options.Map<ValidationException>((ctx, ex) =>
        {
            var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

            var errors = ex.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray());

            return factory.CreateValidationProblemDetails(ctx, errors, StatusCodes.Status400BadRequest);
        });
    }

    public static void MapFanaticsExceptions(this ProblemDetailsOptions options)
    {
        options.MapToStatusCode<EntityNotFoundException>(StatusCodes.Status404NotFound);
        options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
    }
}