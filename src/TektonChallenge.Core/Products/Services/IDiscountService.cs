namespace TektonChallenge.Core.Products.Services;

public interface IDiscountService
{
    Task<IEnumerable<DiscountData>> GetCurrentDiscountsAsync(CancellationToken cancellationToken);
}