using System.Net.Http.Json;
using TektonChallenge.Core.Products.Services;

namespace TektonChallenge.Infrastructure.Services;

public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;

    public DiscountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<DiscountData>> GetCurrentDiscountsAsync(CancellationToken cancellationToken)
    {
        return (await _httpClient.GetFromJsonAsync<IEnumerable<DiscountData>>("discounts", cancellationToken))!;
    }
}