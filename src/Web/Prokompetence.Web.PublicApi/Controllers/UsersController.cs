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
    private readonly SecurityTokenHandler securityTokenHandler;
    private readonly AuthenticationOptions authenticationOptions;

    public UsersController(
        IUsersService usersService,
        SecurityTokenHandler securityTokenHandler,
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

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, dto.Login)
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
        return Ok(new AccessTokenDto(
            accessToken,
            result.RefreshToken ?? throw new InvalidOperationException()
        ));
    }
}