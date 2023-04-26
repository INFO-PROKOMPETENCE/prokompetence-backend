using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Configs;

public sealed class KeyTechnologyConfig : IEntityTypeConfiguration<KeyTechnology>
{
    public void Configure(EntityTypeBuilder<KeyTechnology> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedNever();
    }
}