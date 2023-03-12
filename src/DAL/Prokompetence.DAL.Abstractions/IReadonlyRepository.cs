using System.Linq.Expressions;

namespace Prokompetence.DAL.Abstractions;

public interface IReadonlyRepository<TEntity>
    where TEntity : Entity
{
    Task<TEntity[]> GetAll(CancellationToken cancellationToken);

    Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<TEntity> Single(CancellationToken cancellationToken);

    Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<TEntity?> SingleOrDefault(CancellationToken cancellationToken);

    Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<TEntity> First(CancellationToken cancellationToken);

    Task<TEntity> First(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<TEntity?> FirstOrDefault(CancellationToken cancellationToken);

    Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<int> Count(CancellationToken cancellationToken);

    Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<long> LongCount(CancellationToken cancellationToken);

    Task<long> LongCount(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<TResult[]> Query<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query,
        CancellationToken cancellationToken);
}