namespace Prokompetence.Common.Configuration;

[Settings]
public sealed record ConnectionStrings(string SqlServerProkompetence, string PostgresProkompetence);