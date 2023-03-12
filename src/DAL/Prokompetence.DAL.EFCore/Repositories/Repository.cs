using Prokompetence.DAL.Abstractions;

namespace Prokompetence.DAL.EFCore.Repositories;

public abstract class Repository<TEntity> : ReadonlyRepository<TEntity>, IRepository<TEntity>
    where TEntity : Entity
{
    protected override bool IsReadOnly => false;

    protected Repository(ProkompetenceDbContext context)
        : base(context)
    {
    }

    public async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken)
    {
        var result = await Items.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await Items.AddRangeAsync(entities, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Items.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        Items.RemoveRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
    }
}