using Recipe.Api.Types;

namespace Recipe.Api.Operations;

[QueryType]
public class Query
{
    public Book GetBook()
        => new Book("C# in depth.", new Author("Jon Skeet"));
}