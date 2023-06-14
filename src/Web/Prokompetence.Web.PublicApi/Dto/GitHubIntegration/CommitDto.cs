namespace Prokompetence.Web.PublicApi.Dto.GitHubIntegration;

public sealed record CommitDto
{
    public string Message { get; init; }
    public string Hash { get; init; }
}