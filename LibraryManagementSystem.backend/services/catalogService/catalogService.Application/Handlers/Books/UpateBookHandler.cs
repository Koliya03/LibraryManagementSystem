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
    public class UpdateBookHandler
    {
        private readonly IEventStore _eventStore;

        public UpdateBookHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Handle(UpdateBookCommand command)
        {
            Book book = await _eventStore.AggregateAsync<Book>(command.BookId);
            if (book == null) throw new Exception("Book not found.");

            BookUpdatedEvent @event = book.UpdateInfo(command.Title, command.Author, command.ISBN);

            _eventStore.Append(command.BookId, @event);
            await _eventStore.SaveChangesAsync();
        }
    }
}
