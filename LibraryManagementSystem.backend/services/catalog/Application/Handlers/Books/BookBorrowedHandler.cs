using Domain.Books.Entities;
using Domain.Books.Events;
using Messages.Borrowing.Events;
using Wolverine.Attributes;
using Wolverine.Marten;

namespace Application.Handlers.Books
{
    public static class BookBorrowedHandler
    {
        [WolverineHandler]
        public static BookBorrowedEvent Handle(BorrowRecordCreatedMessage message, [WriteAggregate] Book book)
        {

            return new BookBorrowedEvent(
                message.BookId
            );
        }
    }
}
