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
            Events,
            OutgoingMessages
        ) Put(
            Guid borrowId,
            [WriteAggregate(nameof(borrowId))] BorrowRecord record)
        {
            if (record.IsReturned)
            {
                return (
                     Results.BadRequest(new { Message = "Book already returned " }),
                     new Events(),
                     new OutgoingMessages()
                 );
            }
                
            var now = DateTime.UtcNow;
            decimal lateFee = 0;

            if (now > record.DueDate)
            {
                lateFee = (now - record.DueDate).Days * 1m;
            }

            var returnedEvent = new BookReturnedEvent(
                borrowId, record.MemberId, record.BookId, now, lateFee
            );


            var outgoing = new OutgoingMessages();

            outgoing.Add(new BookReturnedMessage(
                borrowId, record.MemberId, record.BookId, now, lateFee
            ));

            var events = new Events(); 
            events.Add(returnedEvent);

            if (record.IsLost)
            {
                var foundLostBookEvent = new BookMarkedFoundEvent(borrowId, record.MemberId, record.BookId, now);
                events.Add(foundLostBookEvent);
                outgoing.Add(new BookFoundMessage(
                 borrowId, record.MemberId, record.BookId, now
               ));
            }

            if (lateFee > 0)
            {
                var feeEvent = new LateFeeIssuedEvent(borrowId, lateFee);

                outgoing.Add(new LateFeeIssuedMessage(
                    borrowId, lateFee
                ));
                events.Add(feeEvent);
            }
            return (
              Results.Ok(new { Message = "Book returned successfully.", LateFee = lateFee }),
              events,
              outgoing
            );


        }
    }
}
