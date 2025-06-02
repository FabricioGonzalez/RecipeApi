using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;
using Recipe.Api.Repositories;

namespace Recipe.Api.DataLoaders;

public static class StepsLoader
{
    [DataLoader]
    public static async Task<Dictionary<Guid, IQueryable<Models.Step>>> GetStepsByIdAsync(
        IReadOnlyList<Guid> productIds,
        QueryContext<Models.Step> context,
        CancellationToken cancellationToken,
        RecipeRepository repository)
        => await repository.GetSteps()
            .Where(t => productIds.Contains(t.RecipeId))
            .With(context)
            .GroupBy(t => t.RecipeId)
            .Select(t => new { t.Key, Items = t.OrderBy(p => p.Number).ToList() })
            .ToDictionaryAsync(t => t.Key, t => t.Items.AsQueryable(), cancellationToken);
}