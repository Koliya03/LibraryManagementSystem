using Domain.Entities;
using Domain.Events;
using Messages;
using Messages.Borrowing.Events;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http
{
    public static class ReturnBookEndpoint
    {
        [WolverinePut("/api/borrowing/records/{borrowId:guid}/return")]
        public static (
            IResult,
            BookReturnedEvent,
            LateFeeIssuedEvent?,
            OutgoingMessages
        ) Put(
            Guid borrowId,
            [WriteAggregate(nameof(borrowId))] BorrowRecord record)
        {
            if (record.IsReturned)
                throw new Exception("Book already returned.");

            if (record.IsLost)
                throw new Exception("Book already reported as lost.");

            var now = DateTime.UtcNow;
            decimal lateFee = 0;

            if (now > record.DueDate)
            {
                lateFee = (now - record.DueDate).Days * 1m;
            }

            var returnedEvent = new BookReturnedEvent(
                borrowId, record.MemberId, record.BookId, now, lateFee
            );

            LateFeeIssuedEvent? feeEvent = null;

            var outgoing = new OutgoingMessages();

            outgoing.Add(new BookReturnedMessage(
                borrowId, record.MemberId, record.BookId, now, lateFee
            ));

            if (lateFee > 0)
            {
                feeEvent = new LateFeeIssuedEvent(borrowId, lateFee);

                outgoing.Add(new LateFeeIssuedMessage(
                    borrowId, lateFee
                ));
            }

            return (
                Results.Ok(new { Message = "Book returned successfully.", LateFee = lateFee }),
                returnedEvent,
                feeEvent,
                outgoing
            );
        }
    }
}
