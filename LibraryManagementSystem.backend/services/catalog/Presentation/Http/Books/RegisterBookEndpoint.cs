using Application.Commands.Books;
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
      public static (IResult, BookRegisteredEvent, BookRegisteredMessage) Post(RegisterBookCommand command,[WriteAggregate(Required = false)] Book book)
        {
            if (string.IsNullOrWhiteSpace(command.Title))
                throw new Exception("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(command.Author))
                throw new Exception("Author cannot be empty.");
            if (string.IsNullOrWhiteSpace(command.ISBN))
                throw new Exception("ISBN cannot be empty.");
            if (command.TotalQuantity <= 0)
                throw new Exception("Quantity must be greater than zero.");
            var bookId = Guid.NewGuid();

            var @event = new BookRegisteredEvent(
                command.BookId,
                command.Title,
                command.Author,
                command.ISBN,
                command.TotalQuantity
            );

            var message = new BookRegisteredMessage(
                command.BookId,
                command.Title,
                command.Author,
                command.ISBN,
                command.TotalQuantity,
                command.TotalQuantity,
                false
            );

            return (
             Results.Created($"/api/catalog/books/{command.BookId}", new { command.BookId }),
             @event,
             message);

        }
    }
}
