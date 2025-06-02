using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using Recipe.Api.Operations;

namespace Recipe.Api.Types;

public class RecipeSubscription  : ObjectTypeExtension<Subscription>
{
    protected override void Configure(
        IObjectTypeDescriptor<Subscription> descriptor
    )
    {
        descriptor
            .Field(RecipeAddedSubscriptor.EventName)
            .Resolve(context => context.GetEventMessage<Models.Recipe>())
            .Subscribe(async context =>
            {
                var receiver = context.Service<ITopicEventReceiver>();
                var subscriptor = context.Service<RecipeAddedSubscriptor>();

                ISourceStream stream =
                    await receiver.SubscribeAsync<Models.Recipe>(RecipeAddedSubscriptor.EventName);

                return stream;
            });
    }
}

public class RecipeAddedSubscriptor
{
    public static string EventName { get; set; } = "recipeAdded";


}