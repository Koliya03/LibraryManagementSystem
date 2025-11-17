using Application.Commands.Books;
using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Books.Events;
using Messages.Borrowing.Books;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Books
{
    public static class UpdateBookEndpoint
    {
        [WolverinePut("/api/catalog/books/{bookId:guid}")]
        public static (IResult, BookUpdatedEvent, BookUpdatedMessage) Put(UpdateBookCommand command,[WriteAggregate] Book book)
        {
            if (string.IsNullOrWhiteSpace(command.Title))
                throw new Exception("Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(command.Author))
                throw new Exception("Author cannot be empty.");

            if (string.IsNullOrWhiteSpace(command.ISBN))
                throw new Exception("ISBN cannot be empty.");

            var evt = new BookUpdatedEvent(
                book.Id,
                command.Title,
                command.Author,
                command.ISBN,
                command.TotalQuantity,
                command.AvailableQuantity
            );

            var message = new BookUpdatedMessage(
                book.Id,
                command.Title,
                command.Author,
                command.ISBN,
                command.TotalQuantity,
                command.AvailableQuantity
            );

            return (
                Results.Ok("Book updated successfully."),
                evt,
                message
            );
        }
    }
}
