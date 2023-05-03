using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Repositories;

public interface IRoleRepository
{
    Task<Role> GetByName(string name, CancellationToken cancellationToken);
    Task AddRoleForUser(Guid userId, int roleId, CancellationToken cancellationToken);
}

public class RoleRepository : IRoleRepository
{
    private readonly IProkompetenceDbContext context;

    public RoleRepository(IProkompetenceDbContext context)
    {
        this.context = context;
    }

    public async Task<Role> GetByName(string name, CancellationToken cancellationToken)
        => await context.Roles.Where(r => r.Name == name).SingleAsync(cancellationToken);

    public async Task AddRoleForUser(Guid userId, int roleId, CancellationToken cancellationToken)
    {
        await context.UserRoles.AddAsync(new UserRole { RoleId = roleId, UserId = userId }, cancellationToken);
    }
}