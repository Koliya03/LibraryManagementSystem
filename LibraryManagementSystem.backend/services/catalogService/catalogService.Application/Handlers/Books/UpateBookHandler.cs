using Application.Commands.Books;
using Application.Interfaces;
using Domain.Books.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;

namespace Application.Handlers.Books
{
    public class UpateBookHandler
    {
        private readonly IBookRepository _repository;
        public UpateBookHandler(IBookRepository repository)
        {  
            _repository = repository;
        }

        public async Task Handle(UpdateBookCommand command,IMessageBus context)
        {
            var book = await _repository.GetByIdAsync(command.BookId);
            if (book == null)
            {
                Console.WriteLine("Book cannot be found");
                return;
            }

            book.Title = command.Title;
            book.Author = command.Author;
            book.TotalQuantity = command.TotalQuantity;
            book.AvailableQuantity = command.AvailableQuantity;

            await _repository.UpdateBookAsync(book);

            await context.PublishAsync(new BookUpdatedEvent(
                book.Id,
                book.Title,
                book.Author,
                book.TotalQuantity,
                book.AvailableQuantity
            ));
        }

    }
}
