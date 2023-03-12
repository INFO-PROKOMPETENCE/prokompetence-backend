using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL.Abstractions;

namespace Prokompetence.DAL.EFCore.Repositories;

public abstract class ReadonlyRepository<TEntity> : IReadonlyRepository<TEntity>
    where TEntity : Entity
{
    protected readonly ProkompetenceDbContext Context;
    protected readonly DbSet<TEntity> Items;
    protected virtual bool IsReadOnly => true;

    protected virtual IQueryable<TEntity> Query =>
        IsReadOnly
            ? Items.AsNoTracking()
            : Items;

    protected ReadonlyRepository(ProkompetenceDbContext context)
    {
        Context = context;
        Items = context.Set<TEntity>();
    }

    public Task<TEntity[]> GetAll(CancellationToken cancellationToken)
        => Query.ToArrayAsync(cancellationToken);

    public Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        => Query.Where(predicate).ToArrayAsync(cancellationToken);

    public Task<TEntity> Single(CancellationToken cancellationToken)
        => Query.SingleAsync(cancellationToken);

    public Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        => Query.Where(predicate).SingleAsync(cancellationToken);

    public Task<TEntity?> SingleOrDefault(CancellationToken cancellationToken)
        => Query.SingleOrDefaultAsync(cancellationToken);

    public Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        => Query.Where(predicate).SingleOrDefaultAsync(cancellationToken);

    public Task<TEntity> First(CancellationToken cancellationToken)
        => Query.FirstAsync(cancellationToken);

    public Task<TEntity> First(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        => Query.Where(predicate).FirstAsync(cancellationToken);

    public Task<TEntity?> FirstOrDefault(CancellationToken cancellationToken)
        => Query.FirstOrDefaultAsync(cancellationToken);

    public Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        => Query.Where(predicate).FirstOrDefaultAsync(cancellationToken);

    public Task<int> Count(CancellationToken cancellationToken)
        => Query.CountAsync(cancellationToken);

    public Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        => Query.Where(predicate).CountAsync(cancellationToken);

    public Task<long> LongCount(CancellationToken cancellationToken)
        => Query.LongCountAsync(cancellationToken);

    public Task<long> LongCount(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        => Query.Where(predicate).LongCountAsync(cancellationToken);

    Task<TResult[]> IReadonlyRepository<TEntity>.Query<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        => query(Query).ToArrayAsync(cancellationToken);
}