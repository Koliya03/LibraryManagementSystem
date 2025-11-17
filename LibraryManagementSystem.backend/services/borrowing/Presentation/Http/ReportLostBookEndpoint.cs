using Domain.Entities;
using Domain.Events;
using Messages.Borrowing.Events;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http
{
    public static class ReportLostBookEndpoint
    {
        [WolverinePut("/api/borrowing/records/{borrowId:guid}/lost")]
        public static (IResult, BookMarkedLostEvent, BookLostMessage) Put(
            Guid borrowId,
            [WriteAggregate(nameof(borrowId))] BorrowRecord record)
        {
            if (record.IsReturned)
                throw new Exception("Book already returned.");

            if (record.IsLost)
                throw new Exception("Book already marked as lost.");

            var evt = new BookMarkedLostEvent(
                borrowId,
                record.MemberId,
                record.BookId,
                DateTime.UtcNow
            );

            var message = new BookLostMessage(
                borrowId,
                record.MemberId,
                record.BookId,
                DateTime.UtcNow
            );

            return (
                Results.Ok("Book marked as lost."),
                evt,
                message
            );
        }
    }
}
