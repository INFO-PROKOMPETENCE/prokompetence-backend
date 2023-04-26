using Prokompetence.DAL.Abstractions;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Repositories;

public interface IRolesRepository : IRepository<UserRole>
{
    Task<UserRole> GetByName(string name, CancellationToken cancellationToken);
}