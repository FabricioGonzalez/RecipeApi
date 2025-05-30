using Microsoft.EntityFrameworkCore;
using Recipe.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddGraphQL().AddTypes();

builder.Services.AddDbContextFactory<RecipeContext>((
    provider
    , optionsBuilder
) => optionsBuilder.UseNpgsql(provider.GetService<IConfiguration>()["ConnectionStrings:DefaultConnectionString"]));

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);