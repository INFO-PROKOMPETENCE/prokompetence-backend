using GitHubJwt;
using Octokit;
using Prokompetence.Clients.GitHub.Builders;
using Prokompetence.Clients.GitHub.Settings;

namespace Prokompetence.Clients.GitHub.Providers;

public interface IGitHubClientProvider
{
    Task<IGitHubClient> GetForRepository(string owner, string repo);
}

public sealed class GitHubClientProvider : IGitHubClientProvider
{
    private readonly IGitHubClientBuilder builder;
    private readonly IGitHubSecretKeyProvider secretKeyProvider;
    private readonly GitHubSettings settings;

    public GitHubClientProvider(
        IGitHubClientBuilder builder,
        IGitHubSecretKeyProvider secretKeyProvider,
        GitHubSettings settings
    )
    {
        this.builder = builder;
        this.secretKeyProvider = secretKeyProvider;
        this.settings = settings;
    }

    public async Task<IGitHubClient> GetForRepository(string owner, string repo)
    {
        var secretKey = await secretKeyProvider.GetSecretKey();
        var jwtFactory = new GitHubJwtFactory(
            new StringPrivateKeySource(secretKey),
            new GitHubJwtFactoryOptions
            {
                AppIntegrationId = settings.AppId,
                ExpirationSeconds = 600
            }
        );
        var jwt = jwtFactory.CreateEncodedJwtToken();
        var clientForGettingAccessToken = builder.Build(jwt);
        var installation =
            await clientForGettingAccessToken.GitHubApps.GetRepositoryInstallationForCurrent(owner, repo);
        var accessToken = await clientForGettingAccessToken.GitHubApps.CreateInstallationToken(installation.Id);
        return builder.Build(accessToken.Token);
    }
}