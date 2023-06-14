using Octokit;
using Octokit.Internal;
using Prokompetence.Clients.GitHub.Settings;

namespace Prokompetence.Clients.GitHub.Builders;

public interface IGitHubClientBuilder
{
    IGitHubClient Build(string token);
}

public sealed class GitHubClientBuilder : IGitHubClientBuilder
{
    private readonly GitHubSettings settings;

    public GitHubClientBuilder(GitHubSettings settings)
    {
        this.settings = settings;
    }

    public IGitHubClient Build(string token)
    {
        var productHeaderValue = new ProductHeaderValue(settings.AppName);
        var credentials = new Credentials(token, AuthenticationType.Bearer);
        ICredentialStore credentialStore = new InMemoryCredentialStore(credentials);
        return new GitHubClient(productHeaderValue, credentialStore);
    }
}