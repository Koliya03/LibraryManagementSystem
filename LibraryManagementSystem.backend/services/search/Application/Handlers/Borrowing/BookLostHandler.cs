using Application.Interfaces;
using Domain.Entities;
using Messages.Borrowing.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Borrowing
{
    public class BookLostHandler
    {
        private readonly ISearchIndex<Book> _books;

        public BookLostHandler(ISearchIndex<Book> books)
        {
            _books = books;
        }

        public async Task Handle(BookLostMessage message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" search RECEIVED lostbook from borrowed:");
            Console.ResetColor();


            var book = await _books.GetByIdAsync(message.BookId);
            if (book == null)
                return;

            if (book.TotalQuantity > 0)
            {
                book.TotalQuantity -= 1;
            }

            await _books.UpdateAsync(book);
        }
    }
}
