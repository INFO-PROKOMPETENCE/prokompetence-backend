namespace Prokompetence.Web.PublicApi.Dto.GitHubIntegration;

public sealed record GitHubIntegrationDto
{
    /// <summary>
    /// Владелец репозитория (либо пользователь, либо название организации)
    /// </summary>
    public string Owner { get; init; }

    /// <summary>
    /// Название репозитория
    /// </summary>
    public string Repository { get; init; }
}