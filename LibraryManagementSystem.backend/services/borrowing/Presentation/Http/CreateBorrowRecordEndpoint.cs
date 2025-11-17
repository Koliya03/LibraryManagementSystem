using Application.DTO;
using Domain.Entities;
using Domain.Events;
using Messages;
using Messages.Borrowing.Events;
using Messages.Borrowing.Requests;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http
{
    public static class CreateBorrowRecordEndpoint
    {
        [WolverinePost("/api/borrowing/records")]
        public static async Task<(IResult, BorrowRecordCreatedEvent, OutgoingMessages)> Post(
            BorrowRecordRequestDto request,
            [WriteAggregate(nameof(request.BorrowId), Required = false)] BorrowRecord? record,
            IMessageContext bus)
        {
            if (request.MemberId == Guid.Empty)
                throw new Exception("MemberId is required.");

            if (request.BookId == Guid.Empty)
                throw new Exception("BookId is required.");

            var memberStatus = await bus.InvokeAsync<MemberStatusDto>(
                new GetMemberStatusRequest(request.MemberId)
            );

            if (!memberStatus.IsActive)
                throw new Exception("Member is not active.");

            var bookAvailability = await bus.InvokeAsync<AvailabilityDto>(
                 new GetBookAvailabilityRequest(request.BookId)
            );

            if (!bookAvailability.IsAvailable)
                throw new Exception("Book is not available.");

            //var borrowId = Guid.NewGuid();
            var borrowDate = DateTime.UtcNow;
            var dueDate = borrowDate.AddDays(14);

            var evt = new BorrowRecordCreatedEvent(
                request.BorrowId,
                request.MemberId,
                request.BookId,
                borrowDate,
                dueDate
            );

            var message = new BorrowRecordCreatedMessage(
                  request.BorrowId,
                 request.MemberId,
                 request.BookId,
                 borrowDate,
                 dueDate
            );
            var outgoing = new OutgoingMessages
            {
                new BorrowRecordCreatedMessage(
                    request.BorrowId,
                    request.MemberId,
                    request.BookId,
                    borrowDate,
                    dueDate
                )
            } ;


            return (
                Results.Created($"/api/borrowing/records/{request.BorrowId}", new { request.BorrowId }),
                evt,
                outgoing
            );
        }
    }
}
