using Domain.Books.Entities;
using Domain.Books.Events;
using Messages.Borrowing.Events;
using Wolverine.Marten;

namespace Presentation.Consumer
{
    public static class ReturnBookMessageHandler
    {
        public static BookReturnedEvent Handle(BookReturnedMessage message, [WriteAggregate] Book book)
        {
            return new BookReturnedEvent(
                message.BookId
            );
        }
    }
}
