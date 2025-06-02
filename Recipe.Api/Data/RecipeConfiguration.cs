using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipe.Api.Data;

internal class RecipeConfiguration : IEntityTypeConfiguration<Models.Recipe>
{
    public void Configure(
        EntityTypeBuilder<Models.Recipe> builder
    )
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd().HasDefaultValue(Guid.CreateVersion7());
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate().IsRequired();

        builder.HasMany(x => x.Ingredients).WithOne().HasForeignKey(key => key.RecipeId);
        builder.HasMany(x => x.Steps).WithOne().HasForeignKey(key => key.RecipeId);

        builder.ToTable("recipes");
    }
}