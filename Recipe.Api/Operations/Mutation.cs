using Recipe.Api.Types;

namespace Recipe.Api.Operations;


[MutationType]
public class Mutation
{
    public async Task<Book> AddBook(
        Book book
    )
    {
        return book;
    }
}