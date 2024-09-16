namespace TektonChallenge.Core.Common.Data;

public interface IRepository<TEntity>
{
    ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Remove(TEntity entityToRemove);
    ValueTask<TEntity?> GetByIdAsync(Ulid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Ulid id, CancellationToken cancellationToken = default);
}