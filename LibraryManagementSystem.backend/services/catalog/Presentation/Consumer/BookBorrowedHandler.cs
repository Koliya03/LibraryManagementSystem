//using Domain.Books.Entities;
//using Domain.Books.Events;
//using Messages.Borrowing.Events;
//using Wolverine.Marten;

//namespace Presentation.Consumer
//{
//    public static class BookBorrowedHandler
//    {
//        public static BookBorrowedEvent Handle(BorrowRecordCreatedMessage message, [WriteAggregate] Book book)
//        {
           
//            return new BookBorrowedEvent(
//                message.BookId
//            );
//        }
//    }
//}
