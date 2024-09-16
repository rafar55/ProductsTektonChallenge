using Microsoft.EntityFrameworkCore;
using TektonChallenge.Core.Common.Data;

namespace TektonChallenge.Infrastructure.Data.Repositories;

internal abstract class BaseRepository<TEntity> : IRepository<TEntity> 
    where TEntity : class
{
    protected readonly AppDbContext _db;

    public BaseRepository(AppDbContext dbContext)
    {
        _db = dbContext;
    }
    
    public async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _db.AddAsync(entity, cancellationToken);
    }

    public void Remove(TEntity entityToRemove)
    {
        _db.Remove(entityToRemove);
    }

    public virtual ValueTask<TEntity?> GetByIdAsync(Ulid id, CancellationToken cancellationToken = default)
    {
        return _db.Set<TEntity>().FindAsync([id], cancellationToken: cancellationToken);
    }
}