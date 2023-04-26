using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Configs;

public sealed class TeamRoleConfig : IEntityTypeConfiguration<TeamRole>
{
    public void Configure(EntityTypeBuilder<TeamRole> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedNever();
    }
}