using Application.Commands.Books;
using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Books.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;


namespace Application.Handlers.Books
{
    public class RegisterBookHandler
    {
        private readonly IBookRepository _repository;

        public RegisterBookHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RegisterBookCommand command, IMessageContext context)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Author = command.Author,
                ISBN = command.ISBN,
                TotalQuantity = command.TotalQuantity,
                AvailableQuantity = command.TotalQuantity,
                IsRetired = false
            };
            await _repository.AddBookAsync(book);

            await context.PublishAsync(new BookRegisteredEvent(
                book.Id,
                book.Title,
                book.Author,
                book.ISBN,
                book.TotalQuantity
            ));

        }
    }
}
