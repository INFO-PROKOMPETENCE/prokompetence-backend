using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Configs;

public sealed class TeamConfig : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasOne(e => e.TeamLead)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}