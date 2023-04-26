using System.Security.Claims;
using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Web.PublicApi.Identity;

public static class ClaimsPrincipalExtensions
{
    public static UserIdentityModel GetUserIdentityModel(this ClaimsPrincipal source)
    {
        var claims = source.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
        return new UserIdentityModel
        {
            Id = Guid.Parse(claims[IdentityConstants.Id]),
            Login = claims[IdentityConstants.Login],
            Role = claims[IdentityConstants.Role]
        };
    }
}