using Prokompetence.Clients.GitHub.Settings;

namespace Prokompetence.Clients.GitHub.Providers;

public interface IGitHubSecretKeyProvider
{
    Task<string> GetSecretKey();
}

public sealed class GitHubSecretKeyProvider : IGitHubSecretKeyProvider
{
    private readonly GitHubSettings settings;

    public GitHubSecretKeyProvider(GitHubSettings settings)
    {
        this.settings = settings;
    }

    public async Task<string> GetSecretKey()
    {
        return await File.ReadAllTextAsync(settings.PathToSecretKey);
    }
}