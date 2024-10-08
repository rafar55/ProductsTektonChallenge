using System.Data;

namespace TektonChallenge.Core.Common.Persistence;

public interface IUnitOfWork
{
    TRepository GetRepository<TRepository>() where TRepository : notnull;
    IDbTransaction BeginTransaction();
    Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
}