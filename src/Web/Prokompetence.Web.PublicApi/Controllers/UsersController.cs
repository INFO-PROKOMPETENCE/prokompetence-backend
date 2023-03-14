using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Registration([FromBody] UserRegistrationDto dto,
        CancellationToken cancellationToken)
    {
        var userRegistrationRequest = dto.Adapt<UserRegistrationRequest>();
        await usersService.RegisterUser(userRegistrationRequest, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Get accessToken and refreshToken by login and password
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

        return Ok(result.Result?.Adapt<AccessTokenDto>());
    }

    /// <summary>
    /// Refresh accessToken
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

        return Ok(result.Result?.Adapt<AccessTokenDto>());
    }

    /// <summary>
    /// Get base information about current user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("current")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken cancellationToken)
    {
        var login = User.Claims.Single(c => c.Type == ClaimTypes.Name).Value;
        var user = await usersService.GetUserByLogin(login, cancellationToken);
        return Ok(user.Adapt<UserDto>());
    }
}