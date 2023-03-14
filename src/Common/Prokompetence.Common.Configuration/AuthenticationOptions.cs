namespace Prokompetence.Common.Configuration;

[Settings("Authentication")]
public sealed record AuthenticationOptions(
    string Issuer,
    string Audience,
    string Key,
    TimeSpan JwtTokenLifeTime
);