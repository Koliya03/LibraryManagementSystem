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
    public class BookFoundHandler
    {
        private readonly ISearchIndex<Book> _books;

        public BookFoundHandler(ISearchIndex<Book> books)
        {
            _books = books;
        }

        public async Task Handle(BookFoundMessage message)
        {
            var book = await _books.GetByIdAsync(message.BookId);
            if (book == null)
                return;

            book.TotalQuantity += 1;

            await _books.UpdateAsync(book);
        }
    }
}
