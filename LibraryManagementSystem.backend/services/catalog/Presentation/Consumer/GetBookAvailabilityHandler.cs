using Application.Interfaces;
using Domain.Books.Entities;
using Messages.Borrowing.Requests;
using Messages.Catalog.Responses;
using System.Net;
using Wolverine.Attributes;

namespace Presentation.Consumer
{
    public static class GetBookAvailabilityHandler
    {
        [MessageTimeout(2)]
        public static async Task<BookAvailabilityResponse> Handle(
            GetBookAvailabilityRequest message,
            IReadStore readStore)
        {
            var book = await readStore.LoadAsync<Book>(message.BookId);

            if (book == null)
            {
                 return new BookAvailabilityResponse(
                    message.BookId,
                    0,
                    false
                 );
            }

            return new BookAvailabilityResponse
            (
                book.Id,
                book.AvailableQuantity,
                book.AvailableQuantity > 0 && !book.IsRetired

            );
                
            
        }
    }
}
