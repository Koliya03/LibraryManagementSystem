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
        public static (IResult, Events, BookLostMessage) Put(
            Guid borrowId,
            [WriteAggregate(nameof(borrowId))] BorrowRecord record)
        {
            if (record.IsReturned)
            {
                return (
                 Results.BadRequest("Book already returned"),
                 null,
                 null
             );
            }


            if (record.IsLost)
            {
                return (
                 Results.BadRequest("Book already returned"),
                 null,
                 null
             );
            }

                var events = new Events();

                events.Add(
                    new BookMarkedLostEvent(
                    borrowId,
                    record.MemberId,
                    record.BookId,
                    DateTime.UtcNow
                    )
                );

                var message = new BookLostMessage(
                    borrowId,
                    record.MemberId,
                    record.BookId,
                    DateTime.UtcNow
                );

                return (
                    Results.Ok("Book marked as lost."),
                    events,
                    message
                );
        }
    }
}

