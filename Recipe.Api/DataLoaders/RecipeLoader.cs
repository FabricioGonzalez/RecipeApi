using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;
using Recipe.Api.Repositories;

namespace Recipe.Api.DataLoaders;

// This is using the source-generated data loader.
public static class RecipeLoader
{
    [DataLoader]
    public static async Task<Dictionary<Guid, Models.Recipe>> GetRecipeByIdAsync(
        IReadOnlyList<Guid> productIds,
        QueryContext<Models.Recipe> context,
        CancellationToken cancellationToken,
        RecipeRepository repository)
        => await repository.GetRecipes()
            .With(context)
            .Where(t => productIds.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
}