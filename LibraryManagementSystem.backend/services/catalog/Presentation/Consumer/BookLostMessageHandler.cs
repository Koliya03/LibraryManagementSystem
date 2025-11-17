using Domain.Books.Entities;
using Domain.Books.Events;
using Messages.Borrowing.Events;
using Wolverine.Marten;

namespace Presentation.Consumer
{
    public static class BookLostMessageHandler
    {
        public static BookLostEvent Handle(BookLostMessage message, [WriteAggregate] Book book)
        {
            return new BookLostEvent(
               message.BookId
           );
        }
    }
}
