using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using TektonChallenge.Core.Common.Cache;

namespace TektonChallenge.Infrastructure.Cache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<TType> GetOrSet<TType>(string key, Func<Task<TType>> onCacheMiss,
        int expirationInMinutes = 5,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedValue != null)
        {
            return JsonSerializer.Deserialize<TType>(cachedValue)!;
        }

        var value = await onCacheMiss();

        var distributedCacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationInMinutes)
        };

        await _distributedCache.SetStringAsync(
            key: key, 
            value: JsonSerializer.Serialize(value), 
            options: distributedCacheEntryOptions,
            cancellationToken);
        
        return value;
    }
}