using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Books.Events;
using Messages.Borrowing.Books;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Books
{
    public static class RetireBookEndpoint
    {
        [WolverinePost("/api/catalog/books/{bookId:guid}/retire")]
        public static (IResult, BookRetiredEvent, BookRetiredMessage) Post(
        [WriteAggregate] Book book)
        {
            var evt = new BookRetiredEvent(book.Id);
            var message = new BookRetiredMessage(book.Id);

            return (
                Results.Ok("Book retired successfully."),
                evt,
                message
            );
        }
    }

}
