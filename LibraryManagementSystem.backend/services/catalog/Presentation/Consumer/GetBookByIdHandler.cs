using Application.Interfaces;
using Domain.Books.Entities;
using Messages.Catalog.Responses;
using Messages.Report;
using Wolverine.Attributes;

namespace Presentation.Consumer
{
    public static class GetBookByIdHandler
    {
        [MessageTimeout(2)]
        public static async Task<BookResponseDto> Handle(
            GetBookByIdRequest message,
            IReadStore readStore)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" catalog RECEIVED request from report:");
            Console.ResetColor();

            var book = await readStore.LoadAsync<Book>(message.BookId);

            if (book == null)
            {
                return null;
            }

            return new BookResponseDto(
                book.Id,
                book.Title,
                book.Author,
                book.ISBN,
                book.TotalQuantity,
                book.AvailableQuantity,
                book.IsRetired
            );
        }
    }
}

