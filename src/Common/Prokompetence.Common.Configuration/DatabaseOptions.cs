namespace Prokompetence.Common.Configuration;

[Settings("Database")]
public sealed record DatabaseOptions(
    string? Mode
);