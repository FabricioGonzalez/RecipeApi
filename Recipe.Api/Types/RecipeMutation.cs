using HotChocolate.Subscriptions;
using Recipe.Api.Models;
using Recipe.Api.Operations;
using Recipe.Api.Repositories;

namespace Recipe.Api.Types;

public class RecipeMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(
        IObjectTypeDescriptor<Mutation> descriptor
    )
    {
        descriptor.Field("addRecipe")
            .Argument("recipeInput", f => f.Type<NonNullType<RecipeInputType>>())
            .Resolve(async (
                ctx
                , ct
            ) =>
            {
                var recipeInput = ctx.ArgumentValue<RecipeInput>("recipeInput");
                var recipeRepository = ctx.Service<RecipeRepository>();
                var topicEventSender = ctx.Service<ITopicEventSender>();
                var recipe = new Models.Recipe()
                {
                    Title = recipeInput.RecipeTitle, Ingredients = recipeInput.Ingredients.Select(it => new Ingredient()
                    {
                        Name = it.Name, Quantity = it.Quantity, MeasumentUnit = it.MeasumentUnit
                    }).AsQueryable()
                    , Steps = recipeInput.Steps.Select(it => new Step()
                    {
                        Number = it.Number, Description = it.Description, Image = it.Image
                    }).AsQueryable()
                };

                var createdRecipe = await recipeRepository.CreateRecipe(recipe, ct);

                if (createdRecipe is null)
                {
                    throw new Exception("Could not create recipe");
                }

                await topicEventSender.SendAsync(RecipeAddedSubscriptor.EventName, createdRecipe, ct);

                return recipe;
            });
    }
}

public class StepsInputType : InputObjectType<StepsInput>;

public class IngredientsInputType : InputObjectType<IngredientsInput>;

public class RecipeInputType : InputObjectType<RecipeInput>
{
    protected override void Configure(
        IInputObjectTypeDescriptor<RecipeInput> descriptor
    )
    {
        descriptor.Field("steps").Type<ListType<StepsInputType>>().DefaultValue(new List<IngredientsInput>());
        ;
        descriptor.Field("ingredients").Type<ListType<IngredientsInputType>>()
            .DefaultValue(new List<IngredientsInput>());
    }
}

public class RecipeInput
{
    public string RecipeTitle { get; set; }
    public IEnumerable<StepsInput> Steps { get; set; }
    public IEnumerable<IngredientsInput> Ingredients { get; set; }
}

public class StepsInput
{
    public int Number { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
}

public class IngredientsInput
{
    public string MeasumentUnit { get; set; }
    public string Quantity { get; set; }
    public string Name { get; set; }
}