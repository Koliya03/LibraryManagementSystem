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
        public static (IResult, Events, OutgoingMessages) MakeAvailable([WriteAggregate] Book book)
        {
            var events = new Events();
            var outgoing = new OutgoingMessages();

            if (book.IsRetired)
            {
                return (
                    Results.BadRequest(new { Message = "Cannot make available. Book is retired." }),
                    events,
                    outgoing
                );
            }

            var evt = new BookMadeAvailable(book.Id);
            events.Add(evt);

            var message = new BookMadeAvailableMessage(book.Id);
            outgoing.Add(message);

            return (
                Results.Ok(new {Message = "Book made available." }),
                events,
                outgoing
            );
        }
    }
}
