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
        private readonly IEventStore _eventStore;

        public RegisterBookHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Guid> Handle(RegisterBookCommand command)
        {
            Guid newId = Guid.NewGuid();

            BookRegisteredEvent e = Book.Register(
                newId,
                command.Title,
                command.Author,
                command.ISBN,
                command.TotalQuantity
            );

            _eventStore.StartStream<Book>(newId, e);
            await _eventStore.SaveChangesAsync();
            return newId;
        }
    }
}
