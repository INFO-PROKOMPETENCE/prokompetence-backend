using System.Security.Claims;
using Prokompetence.Common.Security.Models;

namespace Prokompetence.Common.Web.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static UserIdentityModel GetUserIdentityModel(this ClaimsPrincipal source)
    {
        var claims = source.Claims
            .GroupBy(claim => claim.Type, claim => claim.Value)
            .ToDictionary(key => key.Key, value => value.ToArray());
        return new UserIdentityModel
        {
            Id = Guid.Parse(claims[IdentityConstants.Id].Single()),
            Login = claims[IdentityConstants.Login].Single(),
            Roles = claims[IdentityConstants.Role]
        };
    }
}