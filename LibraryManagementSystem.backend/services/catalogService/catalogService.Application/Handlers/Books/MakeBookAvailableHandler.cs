using Application.Commands.Books;
using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Books.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Books
{
    public class MakeBookAvailableHandler
    {
        private readonly IEventStore _eventStore;

        public MakeBookAvailableHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Handle(MakeBookAvailableCommand command)
        {
            Book book = await _eventStore.AggregateAsync<Book>(command.BookId);
            if (book == null) throw new Exception("Book not found.");

            BookMadeAvailable @event = book.MakeAvailable();

            _eventStore.Append(command.BookId, @event);
            await _eventStore.SaveChangesAsync();
        }
    }
}
