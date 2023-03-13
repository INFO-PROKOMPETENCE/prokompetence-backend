using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prokompetence.Common.Configuration;
using Prokompetence.Common.Security;
using Prokompetence.Model.PublicApi.Models.Users;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Users;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUsersService usersService;
    private readonly JwtSecurityTokenHandler securityTokenHandler;
    private readonly AuthenticationOptions authenticationOptions;

    public UsersController(
        IUsersService usersService,
        JwtSecurityTokenHandler securityTokenHandler,
        AuthenticationOptions authenticationOptions
    )
    {
        this.usersService = usersService;
        this.securityTokenHandler = securityTokenHandler;
        this.authenticationOptions = authenticationOptions;
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

        var accessToken = GenerateAccessToken(dto.Login);
        return Ok(new AccessTokenDto(
            accessToken,
            result.RefreshToken ?? throw new InvalidOperationException()
        ));
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<ActionResult<AccessTokenDto>> RefreshToken([FromBody] RefreshTokenDto dto,
        CancellationToken cancellationToken)
    {
        var token = securityTokenHandler.ReadJwtToken(dto.AccessToken);
        var user = new ClaimsPrincipal(new ClaimsIdentity(token.Claims));
        var login = user.Claims.Single(c => c.Type == ClaimTypes.Name).Value;
        var result = await usersService.RefreshToken(login, dto.RefreshToken, cancellationToken);
        if (!result.Success)
        {
            return BadRequest("Invalid refresh token");
        }

        var accessToken = GenerateAccessToken(login);
        return Ok(new AccessTokenDto(
            accessToken,
            result.RefreshToken ?? throw new InvalidOperationException()
        ));
    }

    private string GenerateAccessToken(string login)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, login)
        };
        var jwt = new JwtSecurityToken
        (
            issuer: authenticationOptions.Issuer,
            audience: authenticationOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(JwtHelper.GetSymmetricSecurityKey(authenticationOptions.Key),
                SecurityAlgorithms.HmacSha256)
        );
        var accessToken = securityTokenHandler.WriteToken(jwt);
        return accessToken;
    }
}