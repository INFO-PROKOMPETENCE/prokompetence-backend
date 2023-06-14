using Microsoft.EntityFrameworkCore;
using Octokit;
using Prokompetence.Clients.GitHub.Providers;
using Prokompetence.Common.Security.Abstractions;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;
using Prokompetence.Model.PublicApi.Models.GitHubIntegration;

namespace Prokompetence.Model.PublicApi.Services;

public interface IGitHubService
{
    Task AddGitHubIntegration(Guid projectId, GitHubIntegrationModel model, CancellationToken cancellationToken);
    Task<CommitModel[]> GetCommits(Guid projectId, CancellationToken cancellationToken);
}

public sealed class GitHubService : IGitHubService
{
    private readonly IProkompetenceDbContext dbContext;
    private readonly Func<IContextUserProvider> userProviderFactory;
    private readonly IGitHubClientProvider gitHubClientProvider;

    public GitHubService(
        IProkompetenceDbContext dbContext,
        Func<IContextUserProvider> userProviderFactory,
        IGitHubClientProvider gitHubClientProvider)
    {
        this.dbContext = dbContext;
        this.userProviderFactory = userProviderFactory;
        this.gitHubClientProvider = gitHubClientProvider;
    }

    public async Task AddGitHubIntegration(Guid projectId, GitHubIntegrationModel model,
        CancellationToken cancellationToken)
    {
        var user = userProviderFactory.Invoke().GetUser();
        var studentInTeam = await dbContext.StudentsInTeam
            .Where(s => s.StudentId == user.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (studentInTeam is not { IsTeamLead: true })
        {
            throw new Exception();
        }

        var project = await dbContext.Projects
            .Where(p => p.Id == projectId)
            .FirstOrDefaultAsync(cancellationToken);
        if (project is null)
        {
            throw new Exception();
        }

        project.GitHubIntegration = new GitHubIntegration
        {
            OwnerName = model.Owner,
            RepositoryName = model.Repository
        };
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<CommitModel[]> GetCommits(Guid projectId, CancellationToken cancellationToken)
    {
        var integration = await dbContext.Projects
            .Where(p => p.Id == projectId)
            .Select(p => p.GitHubIntegration)
            .FirstOrDefaultAsync(cancellationToken);
        if (integration is null)
        {
            return Array.Empty<CommitModel>();
        }

        var owner = integration.OwnerName;
        var repository = integration.RepositoryName;
        var client = await gitHubClientProvider.GetForRepository(owner, repository);
        var commits = await client.Repository.Commit.GetAll(owner, repository);

        return commits.Select(c => new CommitModel
        {
            Message = c.Commit.Message,
            Hash = c.Sha
        }).ToArray();
    }
}