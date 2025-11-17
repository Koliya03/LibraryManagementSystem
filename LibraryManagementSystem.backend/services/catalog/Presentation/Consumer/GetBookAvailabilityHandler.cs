using Application.DTO;
using Application.Interfaces;
using Domain.Books.Entities;
using Messages.Borrowing.Requests;
using Wolverine.Attributes;

namespace Presentation.Consumer
{
    public static class GetBookAvailabilityHandler
    {
        [MessageTimeout(1)]
        public static async Task<AvailabilityDto> Handle(
            GetBookAvailabilityRequest message,
            IReadStore readStore)
        {
            var book = await readStore.LoadAsync<Book>(message.BookId);

            if (book == null)
            {
                return new AvailabilityDto
                {
                    BookId = message.BookId,
                    AvailableQuantity = 0,
                    IsAvailable = false
                };
            }

            return new AvailabilityDto
            {
                BookId = book.Id,
                AvailableQuantity = book.AvailableQuantity,
                IsAvailable = book.AvailableQuantity > 0 && !book.IsRetired
            };
        }
    }
}
