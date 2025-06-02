using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;
using Recipe.Api.Repositories;

namespace Recipe.Api.DataLoaders;

public static class IngredientsLoader
{
    [DataLoader]
    public static async Task<Dictionary<Guid, IQueryable<Models.Ingredient>>> GetIngredientsByIdAsync(
        IReadOnlyList<Guid> productIds,
        QueryContext<Models.Ingredient> context,
        CancellationToken cancellationToken,
        RecipeRepository repository)
        => await repository.GetIngredients()
            .Where(t => productIds.Contains(t.RecipeId))
            .With(context)
            .GroupBy(t => t.RecipeId)
            .Select(t => new { t.Key, Items = t.OrderBy(p => p.Name).ToList() })
            .ToDictionaryAsync(t => t.Key, t => t.Items.AsQueryable(), cancellationToken);
}