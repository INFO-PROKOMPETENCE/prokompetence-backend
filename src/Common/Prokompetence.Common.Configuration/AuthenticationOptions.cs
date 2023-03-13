namespace Prokompetence.Common.Configuration;

public sealed record AuthenticationOptions(
    string Issuer,
    string Audience,
    string Key
);