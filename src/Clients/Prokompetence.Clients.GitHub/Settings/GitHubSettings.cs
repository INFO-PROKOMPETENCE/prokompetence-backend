namespace Prokompetence.Clients.GitHub.Settings;

[Common.Configuration.Settings("GitHub")]
public sealed record GitHubSettings
{
    public int AppId { get; init; }
    public string AppName { get; init; }
    public string PathToSecretKey { get; init; }
}