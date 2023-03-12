using Prokompetence.DAL.Abstractions;

namespace Prokompetence.DAL.Extensions;

public static class ReadonlyRepositoryExtensions
{
    public static async Task<TEntity> GetById<TEntity, TId>(
        this IReadonlyRepository<TEntity> source,
        TId id,
        CancellationToken cancellationToken
    )
        where TEntity : Entity<TId>
    {
        return await source.FindById(id, cancellationToken) ??
               throw new Exception($"Entity with id {id} not found in database");
    }

    public static async Task<TEntity?> FindById<TEntity, TId>(
        this IReadonlyRepository<TEntity> source,
        TId id,
        CancellationToken cancellationToken
    )
        where TEntity : Entity<TId>
    {
        return await source.SingleOrDefault(e => Equals(e.Id, id), cancellationToken);
    }
}