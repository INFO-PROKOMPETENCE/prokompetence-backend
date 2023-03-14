namespace Prokompetence.Web.PublicApi.Dto.Users;

public sealed record AccessTokenDto(
    string AccessToken,
    string RefreshToken,
    DateTime Expires
);