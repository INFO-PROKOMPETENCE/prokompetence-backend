namespace Prokompetence.Web.PublicApi.Dto.Users;

public sealed record RefreshTokenDto(
    string AccessToken,
    string RefreshToken
);