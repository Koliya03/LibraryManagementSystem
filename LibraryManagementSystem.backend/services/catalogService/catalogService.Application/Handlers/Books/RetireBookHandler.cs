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
    public class RetireBookHandler
    {
        private readonly IEventStore _eventStore;

        public RetireBookHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Handle(RetireBookCommand command)
        {
            Book book = await _eventStore.AggregateAsync<Book>(command.BookId);
            if (book == null) throw new Exception("Book not found.");

            BookRetiredEvent @event = book.Retire();

            _eventStore.Append(command.BookId, @event);
            await _eventStore.SaveChangesAsync();
        }
    }
}
