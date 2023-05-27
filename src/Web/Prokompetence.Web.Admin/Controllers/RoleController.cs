using Microsoft.AspNetCore.Mvc;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;
using Prokompetence.Web.Admin.Dto.Role;

namespace Prokompetence.Web.Admin.Controllers;

public class RoleController
{
    private readonly IProkompetenceDbContext dbContext;

    public RoleController(IProkompetenceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    [Route("api/admin/roles")]
    public async Task AddRoleForUser([FromBody] AddRoleForUserDto body)
    {
        var role = dbContext.Roles.First(r => r.Name.ToLower() == body.Role.ToLower());
        if (!dbContext.UserRoles.Any(ur => ur.UserId == body.UserId && ur.RoleId == role.Id))
        {
            dbContext.UserRoles.Add(new UserRole { RoleId = role.Id, UserId = body.UserId });
            await dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}