using System.Security.Claims;
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
    public async Task<IActionResult> Registration([FromBody] UserRegistrationDto dto,
        CancellationToken cancellationToken)
    {
        var userRegistrationRequest = dto.Adapt<UserRegistrationRequest>();
        await usersService.RegisterUser(userRegistrationRequest, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AccessTokenDto>> Login([FromBody] UserLoginDto dto,
        CancellationToken cancellationToken)
    {
        var result = await usersService.SignIn(dto.Login, dto.Password, cancellationToken);
        if (!result.Success)
        {
            return Unauthorized();
        }

        return Ok(new AccessTokenDto(
            result.AccessToken ?? throw new InvalidOperationException(),
            result.RefreshToken ?? throw new InvalidOperationException()
        ));
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<ActionResult<AccessTokenDto>> RefreshToken([FromBody] RefreshTokenDto dto,
        CancellationToken cancellationToken)
    {
        var result = await usersService.RefreshToken(dto.AccessToken, dto.RefreshToken, cancellationToken);
        if (!result.Success)
        {
            return BadRequest("Invalid refresh token");
        }

        return Ok(new AccessTokenDto(
            result.AccessToken ?? throw new InvalidOperationException(),
            result.RefreshToken ?? throw new InvalidOperationException()
        ));
    }
}