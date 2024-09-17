namespace TektonChallenge.Core.Common.Cache;

public interface ICacheService
{
    Task<TType> GetOrSet<TType>(string key, Func<Task<TType>> onCacheMiss,
        int expirationInMinutes = 5,
        CancellationToken cancellationToken = default);
}