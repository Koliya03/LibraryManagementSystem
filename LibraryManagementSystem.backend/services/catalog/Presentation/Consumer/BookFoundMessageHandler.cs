using Domain.Books.Entities;
using Domain.Books.Events;
using Messages.Borrowing.Events;
using Wolverine.Marten;

namespace Presentation.Consumer
{
    public static class BookFoundMessageHandler
    {
       
        public static BookFoundEvent Handle(BookFoundMessage message, [WriteAggregate] Book book)
        {
            return new BookFoundEvent(
                message.BookId
            );
        }
        
    }
}
