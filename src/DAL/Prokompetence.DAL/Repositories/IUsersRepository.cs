using Prokompetence.DAL.Abstractions;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Repositories;

public interface IUsersRepository : IRepository<User>
{
    Task<User?> FindByLogin(string name, CancellationToken cancellationToken);
}