using System.Text.Json.Serialization;

namespace TektonChallenge.Core.Products.Services;

public record DiscountData
{
    [JsonPropertyName("id")]
    public required string DiscountId { get; init; }

    public required string ProductId { get; init; }

    [JsonPropertyName("discountPercentage")]
    public required decimal Percentage { get; init; }
}