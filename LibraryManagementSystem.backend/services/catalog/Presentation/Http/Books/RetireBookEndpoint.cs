using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Books.Events;
using Messages.Catalog.Events.Books;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Books
{
    public static class RetireBookEndpoint
    {
        [WolverinePost("/api/catalog/books/{bookId:guid}/retire")]
        public static (IResult, Events, OutgoingMessages) Post(
        [WriteAggregate] Book book)
        {
            var events = new Events();
            var outgoing = new OutgoingMessages();

            var evt = new BookRetiredEvent(book.Id);
            events.Add(evt);

            var message = new BookRetiredMessage(book.Id);
            outgoing.Add(message);

            return (
                Results.Ok(new { Message = "Book retired successfully." }),
                events,
                outgoing
            );
        }
    }

}
