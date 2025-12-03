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
    public static class BookRegisteredHandler
    {
        public static Task Handle(BookRegisteredMessage message, ISearchIndex<Book> _books)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" registered a book:");
            Console.ResetColor();

            var doc = new Book
            {
                Id = message.BookId,
                Title = message.Title,
                Author = message.Author,
                ISBN = message.ISBN,
                TotalQuantity = message.TotalQuantity,
                AvailableQuantity = message.AvailableQuantity,
                IsRetired = message.IsRetired
            };

            return _books.IndexAsync(doc);
        }
    }
}
