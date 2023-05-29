using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;

namespace Prokompetence.Web.Admin.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IProkompetenceDbContext dbContext;

    public UserController(IProkompetenceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    [Route("api/admin/users")]
    public async Task<User[]> GetUsers()
    {
        return await dbContext.Users.ToArrayAsync();
    }
}