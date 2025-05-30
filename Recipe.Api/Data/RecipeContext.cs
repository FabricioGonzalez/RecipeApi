using Microsoft.EntityFrameworkCore;

namespace Recipe.Api.Data;

public class RecipeContext(DbContextOptions<RecipeContext> options) : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder
    )
    {
        modelBuilder
            .ApplyConfiguration(new RecipeConfiguration())
            .ApplyConfiguration(new IngredientConfiguration())
            .ApplyConfiguration(new StepConfiguration());
    }
}