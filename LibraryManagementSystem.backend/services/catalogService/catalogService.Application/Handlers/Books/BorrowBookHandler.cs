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
    public class BorrowBookHandler
    {
        private readonly IEventStore _eventStore;

        public BorrowBookHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Handle(BorrowBooksCommand command)
        {
            Book book = await _eventStore.AggregateAsync<Book>(command.BookId);
            if (book == null) throw new Exception("Book not found.");

            BookBorrowedEvent @event = book.Borrow(command.Quantity);

            _eventStore.Append(command.BookId, @event);
            await _eventStore.SaveChangesAsync();
        }
    }
}
