using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Api.Models;

namespace Recipe.Api.Data;

internal class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(
        EntityTypeBuilder<Ingredient> builder
    )
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate().IsRequired();

        builder.ToTable("ingredients");
    }
}