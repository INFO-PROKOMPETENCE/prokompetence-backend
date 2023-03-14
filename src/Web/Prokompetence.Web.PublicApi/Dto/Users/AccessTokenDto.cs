namespace Prokompetence.Web.PublicApi.Dto.Users;

/// <summary>
/// Dto for provide accessToken and refreshToken
/// </summary>
/// <param name="AccessToken"></param>
/// <param name="RefreshToken"></param>
/// <param name="Expires">When the token expires</param>
public sealed record AccessTokenDto(
    string AccessToken,
    string RefreshToken,
    DateTime Expires
);