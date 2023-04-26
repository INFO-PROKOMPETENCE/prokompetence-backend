namespace Prokompetence.DAL;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}