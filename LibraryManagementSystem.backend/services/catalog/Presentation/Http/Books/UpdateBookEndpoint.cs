using Application.DTO;
using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Books.Events;
using Messages.Catalog.Events.Books;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Books
{
    public static class UpdateBookEndpoint
    {
        [WolverinePut("/api/catalog/books/{bookId:guid}")]
        public static (IResult, Events, OutgoingMessages) Put(UpdateBookDto updateBook,[WriteAggregate] Book book)
        {
            var events = new Events();
            var outgoing = new OutgoingMessages();

            if (string.IsNullOrWhiteSpace(updateBook.Title))
                return (Results.BadRequest(new { Message = "Title cannot be empty." }), events, outgoing);

            if (string.IsNullOrWhiteSpace(updateBook.Author))
                return (Results.BadRequest(new { Message = "Author cannot be empty." }), events, outgoing);

            if (string.IsNullOrWhiteSpace(updateBook.ISBN))
                return (Results.BadRequest(new { Message = "ISBN cannot be empty." }), events, outgoing);


            var evt = new BookUpdatedEvent(
                book.Id,
                updateBook.Title,
                updateBook.Author,
                updateBook.ISBN,
                updateBook.TotalQuantity,
                updateBook.AvailableQuantity
            );

            events.Add(evt);

            var message = new BookUpdatedMessage(
                book.Id,
                updateBook.Title,
                updateBook.Author,
                updateBook.ISBN,
                updateBook.TotalQuantity,
                updateBook.AvailableQuantity
            );
            outgoing.Add(message);

            return (
                Results.Ok(new { Message = "Book updated successfully." }),
                events,
                outgoing
            );
        }
    }
}
