using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Configs;

public sealed class RoleConfig : IEntityTypeConfiguration<Role>
{
    private static readonly Role[] Seed =
    {
        new() { Id = 1, Name = "User" },
        new() { Id = 2, Name = "Student" },
        new() { Id = 3, Name = "Customer" },
        new() { Id = 4, Name = "Admin" }
    };

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.HasData(Seed);
    }
}