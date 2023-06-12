using GitHubJwt;

namespace Prokompetence.Clients.GitHub.Providers;

public sealed class StringPrivateKeySource : IPrivateKeySource
{
    private readonly string key;

    public StringPrivateKeySource(string key)
    {
        this.key = key ?? throw new ArgumentNullException(nameof(key));
    }

    public TextReader GetPrivateKeyReader()
    {
        return new StringReader(key);
    }
}