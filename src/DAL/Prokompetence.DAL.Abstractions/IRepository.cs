namespace Prokompetence.DAL.Abstractions;

public interface IRepository<TEntity> : IReadonlyRepository<TEntity>
    where TEntity : Entity
{
    Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken);
    Task Update(TEntity entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
}