namespace Prokompetence.Model.PublicApi.Models.GitHubIntegration;

public sealed class GitHubIntegrationModel
{
    /// <summary>
    /// Владелец репозитория (либо пользователь, либо название организации)
    /// </summary>
    public string Owner { get; set; }

    /// <summary>
    /// Название репозитория
    /// </summary>
    public string Repository { get; set; }
}