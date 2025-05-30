namespace Recipe.Api.Models;

public class Ingredient : BaseEntity
{
    public string MeasumentUnit { get; set; }
    public string Quantity { get; set; }
    public string Name { get; set; }
    public Guid RecipeId { get; set; }
}