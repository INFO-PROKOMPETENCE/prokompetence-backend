namespace Prokompetence.Web.PublicApi.Dto.Users;

/// <summary>
/// Dto for refresh token
/// </summary>
/// <param name="RefreshToken"></param>
public sealed record RefreshTokenDto(
    string RefreshToken
);