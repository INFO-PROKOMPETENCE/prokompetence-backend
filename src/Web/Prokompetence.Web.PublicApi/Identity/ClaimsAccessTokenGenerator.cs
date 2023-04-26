using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Prokompetence.Common.Configuration;
using Prokompetence.Common.Security;
using Prokompetence.Model.PublicApi.Interfaces;
using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Web.PublicApi.Identity;

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
            new(IdentityConstants.Id, userIdentity.Id.ToString()),
            new(IdentityConstants.Login, userIdentity.Login),
            new(IdentityConstants.Role, userIdentity.Role)
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
}