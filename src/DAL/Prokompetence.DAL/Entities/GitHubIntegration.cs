namespace Prokompetence.DAL.Entities;

public sealed class GitHubIntegration
{
    public Guid ProjectId { get; set; }
    public string OwnerName { get; set; }
    public string RepositoryName { get; set; }

    public Project Project { get; set; }
}