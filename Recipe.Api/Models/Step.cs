namespace Recipe.Api.Models;

public class Step : BaseEntity
{
    public int Number { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
    public Guid RecipeId { get; set; }
}