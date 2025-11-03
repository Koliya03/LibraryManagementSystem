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
    public class RetireBookHandler
    {
        private readonly IBookRepository _repository;

        public RetireBookHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RetireBookCommand command, IMessageBus context)
        {
            var book = await _repository.GetByIdAsync(command.BookId);
            if (book != null)
            {
                book.IsRetired = true;
            }
            await _repository.UpdateBookAsync(book);

            await context.PublishAsync(new BookRetiredEvent(book.Id));
        }
    }
}
