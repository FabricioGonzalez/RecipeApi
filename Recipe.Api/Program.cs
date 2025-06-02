using Microsoft.EntityFrameworkCore;
using Recipe.Api.Data;
using Recipe.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.AddGraphQL()
    .AddQueryType()
    .AddMutationType()
    .AddSubscriptionType()
    .AddTypes()
    .AddInMemorySubscriptions()
    .AddQueryContext()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

builder.Services.AddTransient<RecipeRepository>();

builder.Services.AddDbContextFactory<RecipeContext>((
    provider
    , optionsBuilder
) => optionsBuilder.UseNpgsql(provider.GetService<IConfiguration>()["ConnectionStrings:DefaultConnectionString"]));

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);