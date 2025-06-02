using Recipe.Api.Types;

namespace Recipe.Api.Operations;

[SubscriptionType]
public class Subscription
{
    [Subscribe]
    public Book ListenBook(
        [EventMessage]   Book book
    ) => book;
}