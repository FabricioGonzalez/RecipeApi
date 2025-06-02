using Microsoft.EntityFrameworkCore;
using Recipe.Api.Data;
using Recipe.Api.Models;

namespace Recipe.Api.Repositories;

public class RecipeRepository
{
    private readonly RecipeContext _context;

    public RecipeRepository(
        RecipeContext context
    )
    {
        _context = context;
    }

    public IQueryable<Step> GetSteps()
    {
        return _context.Set<Step>();
    }

    public IQueryable<Ingredient> GetIngredients()
    {
        return _context.Set<Ingredient>();
    }


    public Models.Recipe GetRecipeById(
        Guid id
    )
    {
        return _context.Set<Models.Recipe>().First(it => it.Id == id);
    }

    public IQueryable<Models.Recipe> GetRecipes()
    {
        return _context.Set<Models.Recipe>();
    }

    public async Task<Models.Recipe?> CreateRecipe(
        Models.Recipe recipe
        , CancellationToken ct
    )
    {
        try
        {
            var createdRecipe = await _context.Set<Models.Recipe>().AddAsync(recipe, ct);

            await _context.SaveChangesAsync(ct);

            return createdRecipe.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}