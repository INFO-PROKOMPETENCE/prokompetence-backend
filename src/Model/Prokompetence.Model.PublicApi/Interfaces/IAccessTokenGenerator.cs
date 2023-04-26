using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Model.PublicApi.Interfaces;

public interface IAccessTokenGenerator
{
    AccessTokenResult GenerateAccessToken(UserIdentityModel userIdentity);
}