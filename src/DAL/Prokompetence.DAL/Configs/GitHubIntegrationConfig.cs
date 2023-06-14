using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Configs;

public sealed class GitHubIntegrationConfig : IEntityTypeConfiguration<GitHubIntegration>
{
    public void Configure(EntityTypeBuilder<GitHubIntegration> builder)
    {
        builder.HasOne(e => e.Project)
            .WithOne(e => e.GitHubIntegration);
        builder.HasKey(e => e.ProjectId);
    }
}