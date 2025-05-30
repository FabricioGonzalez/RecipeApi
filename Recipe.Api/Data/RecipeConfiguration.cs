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
        builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate().IsRequired();

        builder.OwnsMany(x => x.Ingredients).WithOwner().HasForeignKey(key => key.RecipeId);
        builder.OwnsMany(x => x.Steps).WithOwner().HasForeignKey(key => key.RecipeId);

        builder.ToTable("recipes");
    }
}