using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Api.Models;

namespace Recipe.Api.Data;

internal class StepConfiguration : IEntityTypeConfiguration<Step>
{
    public void Configure(
        EntityTypeBuilder<Step> builder
    )
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValue(Guid.CreateVersion7());

        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate().IsRequired();

        builder.ToTable("steps");
    }
}