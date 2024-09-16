using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using TektonChallenge.Core.Common.Persistence;

namespace TektonChallenge.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AppDbContext _dbContext;

    public UnitOfWork(IServiceProvider serviceProvider, AppDbContext dbContext)
    {
        _serviceProvider = serviceProvider;
        _dbContext = dbContext;
    }

    public TRepository GetRepository<TRepository>()
        where TRepository : notnull
    {
        return _serviceProvider.GetRequiredService<TRepository>();
    }

    public IDbTransaction BeginTransaction()
    {
        return _dbContext.Database.BeginTransaction().GetDbTransaction();
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}