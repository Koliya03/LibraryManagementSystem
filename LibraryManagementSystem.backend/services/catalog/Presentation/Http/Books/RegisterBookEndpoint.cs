using Application.DTO;
using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Books.Events;
using JasperFx.Events;
using Marten;
using Messages.Catalog.Events.Books;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Books
{
    public static class RegisterBookEndpoint
    {
      [WolverinePost("/api/catalog/books")]
      public static (IResult, Events, OutgoingMessages) Post(RegisterBookDto registerBook,[WriteAggregate(Required = false)] Book book)
        {
            var events = new Events();
            var outgoing = new OutgoingMessages();

            if (book != null)
            {
                return (
                    Results.BadRequest(new { Message = $"Book with id {registerBook.BookId} already exists." }),
                    events,
                    outgoing
                );
            }

            if (string.IsNullOrWhiteSpace(registerBook.Title))
                return (Results.BadRequest(new { Message = "Title cannot be empty." }), events, outgoing);

            if (string.IsNullOrWhiteSpace(registerBook.Author))
                return (Results.BadRequest(new { Message = "Author cannot be empty." }), events, outgoing);

            if (string.IsNullOrWhiteSpace(registerBook.ISBN))
                return (Results.BadRequest(new { Message = "ISBN cannot be empty." }), events, outgoing);

            if (registerBook.TotalQuantity <= 0)
                return (Results.BadRequest(new { Message = "Quantity must be greater than zero." }), events, outgoing);

            var @event = new BookRegisteredEvent(
                registerBook.BookId,
                registerBook.Title,
                registerBook.Author,
                registerBook.ISBN,
                registerBook.TotalQuantity
            );
            events.Add(@event);

            var message = new BookRegisteredMessage(
                registerBook.BookId,
                registerBook.Title,
                registerBook.Author,
                registerBook.ISBN,
                registerBook.TotalQuantity,
                registerBook.TotalQuantity,
                false
            );
            outgoing.Add(message);


            return (
             Results.Created($"/api/catalog/books/{registerBook.BookId}", new { registerBook.BookId }),
             events,
             outgoing);
      }
    }
}
