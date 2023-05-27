using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokompetence.Common.Security.Models;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Configs;

public sealed class RoleConfig : IEntityTypeConfiguration<Role>
{
    private static readonly Role[] Seed =
        UserRoleConstants.Roles.Select((role, id) => new Role { Id = id + 1, Name = role }).ToArray();

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.HasData(Seed);
    }
}