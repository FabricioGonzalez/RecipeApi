namespace Recipe.Api.Models;

public class Recipe : BaseEntity
{
    public string Title { get; set; }

    public IQueryable<Ingredient> Ingredients { get; set; }
    public IQueryable<Step> Steps { get; set; }
}