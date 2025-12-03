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
    public class BookRetiredHandler
    {
        private readonly ISearchIndex<Book> _books;

        public BookRetiredHandler(ISearchIndex<Book> books)
        {
            _books = books;
        }

        public async Task Handle(BookRetiredMessage message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" retired a book:");
            Console.ResetColor();

            var book = await _books.GetByIdAsync(message.BookId);
            if (book == null)
                return;

            book.IsRetired = true;

            await _books.UpdateAsync(book);
        }
    }
}
