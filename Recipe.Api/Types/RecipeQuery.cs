using GreenDonut.Data;
using HotChocolate.Types.Pagination;
using Recipe.Api.DataLoaders;
using Recipe.Api.Models;
using Recipe.Api.Operations;
using Recipe.Api.Repositories;

namespace Recipe.Api.Types;

public class RecipeQuery : ObjectTypeExtension<Query>
{
    protected override void Configure(
        IObjectTypeDescriptor<Query> descriptor
    )
    {
        descriptor.Field("recipe")
            .Argument("id", a => a.Type<NonNullType<UuidType>>())
            // .UseProjection()
            .Resolve((
                ctx
                , ct
            ) =>
            {
                var id = ctx.ArgumentValue<Guid>("id");

                var productById = ctx.Service<IRecipeByIdDataLoader>();

                return productById.LoadAsync(id, ct);
            });

        descriptor.Field("recipes")
            .UsePaging<RecipeType>()
            // .UseProjection<Models.Recipe>()
            .Resolve(async (
                ctx
                , ct
            ) =>
            {
                // var context = ctx.Service<QueryContext<Models.Recipe>>();
                var products = ctx.Service<RecipeRepository>();

                return await products.GetRecipes().ApplyCursorPaginationAsync(ctx);
            });
    }
}

public class RecipeType : ObjectType<Models.Recipe>
{
    protected override void Configure(
        IObjectTypeDescriptor<Models.Recipe> descriptor
    )
    {
        descriptor
            .Field("ingredients")
            // .UseProjection<Ingredient>()
            .Resolve((
                ctx
                , ct
            ) => ctx.Service<IIngredientsByIdDataLoader>().LoadAsync(ctx.Parent<Models.Recipe>().Id, ct));
        descriptor
            .Field("steps")
            // .UseProjection<Step>()
            .Resolve((
                ctx
                , ct
            ) => ctx.Service<IStepsByIdDataLoader>().LoadAsync(ctx.Parent<Models.Recipe>().Id, ct));
    }
}