using Mapster;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Models.Users;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Users;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUsersService usersService;

    public UsersController(IUsersService usersService)
    {
        this.usersService = usersService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Registration([FromBody] UserRegistrationDto dto, CancellationToken cancellationToken)
    {
        var userRegistrationRequest = dto.Adapt<UserRegistrationRequest>();
        await usersService.RegisterUser(userRegistrationRequest, cancellationToken);
        return NoContent();
    }
}