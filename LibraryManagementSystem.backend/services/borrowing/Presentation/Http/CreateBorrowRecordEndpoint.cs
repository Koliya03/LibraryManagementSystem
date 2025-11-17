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
        public static async Task<(IResult, Events, OutgoingMessages)> Post(
            BorrowRecordRequestDto request,
            [WriteAggregate(nameof(request.BorrowId), Required = false)] BorrowRecord? record,
            IMessageContext bus)
        {
            var events = new Events();
            var outgoing = new OutgoingMessages();

            if (request.MemberId == Guid.Empty)
            {
                return (
                    Results.BadRequest("MemberId is required"),
                    events,
                    outgoing
                );
            }

            if (request.BookId == Guid.Empty)
            {
                return (
                    Results.BadRequest("BookId is required."),
                    events,
                    outgoing
                );
            }
            

            var memberStatus = await bus.InvokeAsync<MemberStatusDto>(
                new GetMemberStatusRequest(request.MemberId)
            );

            if (!memberStatus.IsActive)
            {
                return (
                    Results.BadRequest("Member is not activated"),
                    events,
                    null
                );
            }

            var bookAvailability = await bus.InvokeAsync<AvailabilityDto>(
                 new GetBookAvailabilityRequest(request.BookId)
            );

            if (!bookAvailability.IsAvailable)
            {
                return (
                  Results.BadRequest("books is not available"),
                  events,
                  null
              );
            }
               

            var borrowId = Guid.NewGuid();
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
            outgoing.Add(
                new BorrowRecordCreatedMessage(
                    request.BorrowId,
                    request.MemberId,
                    request.BookId,
                    borrowDate,
                    dueDate
                )
            );

            return (
                Results.Created($"/api/borrowing/records/{request.BorrowId}", new { request.BorrowId }),
                events,
                outgoing
            );
        }
    }
}
