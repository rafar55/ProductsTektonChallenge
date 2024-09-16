namespace TektonChallenge.Core.Common.Persistence;

public interface IRepository<TEntity>
    where TEntity : class
{
    ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Remove(TEntity entityToRemove);
    ValueTask<TEntity?> GetByIdAsync(Ulid id, CancellationToken cancellationToken = default);
}