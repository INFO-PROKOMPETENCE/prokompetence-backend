namespace Prokompetence.DAL.Abstractions;

public abstract class Entity
{
}

public abstract class Entity<TId> : Entity
{
    public TId Id { get; set; }
}