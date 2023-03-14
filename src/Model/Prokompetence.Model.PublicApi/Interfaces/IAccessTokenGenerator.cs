using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Model.PublicApi.Interfaces;

public interface IAccessTokenGenerator
{
    string GenerateAccessToken(UserIdentityModel userIdentity);
    UserIdentityModel? TryGetUserModelFromAccessToken(string accessToken);
}