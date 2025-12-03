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
    public class BookMadeAvailableHandler
    {
        private readonly ISearchIndex<Book> _books;

        public BookMadeAvailableHandler(ISearchIndex<Book> books)
        {
            _books = books;
        }

        public async Task Handle(BookMadeAvailableMessage message)
        {
            var book = await _books.GetByIdAsync(message.BookId);
            if (book == null)
                return;

            book.IsRetired = false;

            await _books.UpdateAsync(book);
        }
    }
}
