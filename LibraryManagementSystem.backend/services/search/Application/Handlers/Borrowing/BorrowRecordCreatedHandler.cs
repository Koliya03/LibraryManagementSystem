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
    public class BorrowRecordCreatedHandler
    {
        private readonly ISearchIndex<Book> _books;

        public BorrowRecordCreatedHandler(ISearchIndex<Book> books)
        {
            _books = books;
        }

        public async Task Handle(BorrowRecordCreatedMessage message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"Borrowed a book:{message.BookId}");
            Console.ResetColor();

            var book = await _books.GetByIdAsync(message.BookId);
            if (book == null)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(" book can not find ");
                Console.ResetColor();

                return;
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" book has been found ");
            Console.ResetColor();


            if (book.AvailableQuantity > 0)
            {
                book.AvailableQuantity -= 1;

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(" book quantity has been deducted ");
                Console.ResetColor();
                await _books.UpdateAsync(book);
            }
            else
            {
                Console.WriteLine("avaiable quantity < 0");
            }
        }
    }
}
