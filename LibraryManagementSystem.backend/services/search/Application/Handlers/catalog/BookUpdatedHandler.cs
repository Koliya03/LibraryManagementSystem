using Application.Interfaces;
using Domain.Entities;
using Messages.Catalog.Events.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.catalog
{
    public class BookUpdatedHandler
    {
        private readonly ISearchIndex<Book> _books;

        public BookUpdatedHandler(ISearchIndex<Book> books)
        {
            _books = books;
        }

        public async Task Handle(BookUpdatedMessage message)
        {
            var book = await _books.GetByIdAsync(message.BookId);
            if (book == null)
                return;

            book.Title = message.Title;
            book.Author = message.Author;
            book.ISBN = message.ISBN;
            book.TotalQuantity = message.TotalQuantity;
            book.AvailableQuantity = message.AvailableQuantity;

            await _books.UpdateAsync(book);
        }
    }
}
