using Prokompetence.DAL.Entities;
using Prokompetence.DAL.Repositories;

namespace Prokompetence.DAL.EFCore.Repositories;

public sealed class RolesRepository : Repository<UserRole>, IRolesRepository
{
    public RolesRepository(ProkompetenceDbContext context)
        : base(context)
    {
    }

    public async Task<UserRole> GetByName(string name, CancellationToken cancellationToken)
        => await Single(r => r.Name == name, cancellationToken);
}