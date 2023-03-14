using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Prokompetence.Common.Configuration;
using Prokompetence.Common.Security;
using Prokompetence.Model.PublicApi.Interfaces;
using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Web.PublicApi.Services;

public sealed class ClaimsAccessTokenGenerator : IAccessTokenGenerator
{
    private readonly JwtSecurityTokenHandler securityTokenHandler;
    private readonly AuthenticationOptions authenticationOptions;

    public ClaimsAccessTokenGenerator(JwtSecurityTokenHandler securityTokenHandler,
        AuthenticationOptions authenticationOptions)
    {
        this.securityTokenHandler = securityTokenHandler;
        this.authenticationOptions = authenticationOptions;
    }

    public AccessTokenResult GenerateAccessToken(UserIdentityModel userIdentity)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userIdentity.Login)
        };
        var expires = DateTime.UtcNow.Add(authenticationOptions.JwtTokenLifeTime);
        var jwt = new JwtSecurityToken
        (
            issuer: authenticationOptions.Issuer,
            audience: authenticationOptions.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(JwtHelper.GetSymmetricSecurityKey(authenticationOptions.Key),
                SecurityAlgorithms.HmacSha256)
        );
        var accessToken = securityTokenHandler.WriteToken(jwt);
        var refreshToken = JwtHelper.GenerateRefreshToken();
        return new AccessTokenResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Expires = expires
        };
    }

    public UserIdentityModel? TryGetUserModelFromAccessToken(string accessToken)
    {
        if (!securityTokenHandler.CanReadToken(accessToken))
        {
            return null;
        }

        var token = securityTokenHandler.ReadJwtToken(accessToken);
        var user = new ClaimsPrincipal(new ClaimsIdentity(token.Claims));
        var loginClaim = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name);
        if (loginClaim == null)
        {
            return null;
        }

        return new UserIdentityModel
        {
            Login = loginClaim.Value
        };
    }
}