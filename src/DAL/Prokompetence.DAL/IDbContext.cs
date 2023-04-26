namespace Prokompetence.DAL;

public interface IDbContext
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}