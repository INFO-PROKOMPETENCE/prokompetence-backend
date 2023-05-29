namespace Prokompetence.Common.Configuration;

[Settings]
public sealed class ConnectionStrings
{
    public string SqlServerProkompetence { get; set; }
    public string PostgresProkompetence { get; set; }
}