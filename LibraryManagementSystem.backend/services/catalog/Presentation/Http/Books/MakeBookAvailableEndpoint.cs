using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Books.Events;
using Messages.Catalog.Events.Books;
using System.Net;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Books
{
    public static class MakeBookAvailableEndpoint
    {
        [WolverinePost("/api/catalog/books/{bookId:guid}/make-available")]
        public static (IResult, BookMadeAvailable, BookMadeAvailableMessage) MakeAvailable([WriteAggregate] Book book)
        {
            var evt = new BookMadeAvailable(book.Id);
            var message = new BookMadeAvailableMessage(book.Id);

            return (
                Results.Ok("Book made available."),
                evt,
                message
            );
        }
    }
}
