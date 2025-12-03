using Application.DTO;
using Domain.Entities;
using Domain.Events;
using Messages;
using Messages.Borrowing.Events;
using Messages.Borrowing.Requests;
using Messages.Catalog.Responses;
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

            if (record != null)
            {
                return (
                    Results.BadRequest(new { Message = "Borrow record already exists." }),
                    events,
                    outgoing
                );
            }

            if (request.MemberId == Guid.Empty)
            {
                return (
                    Results.BadRequest(new { Message = "MemberId is required" }),
                    events,
                    outgoing
                );
            }

            if (request.BookId == Guid.Empty)
            {
                return (
                    Results.BadRequest(new { Message = "BookId is required." }),
                    events,
                    outgoing
                );
            }
            

            var memberStatus = await bus.InvokeAsync<MemberStatusResponse>(
                new GetMemberStatusRequest(request.MemberId)
            );

            if (!memberStatus.IsActive)
            {
                return (
                    Results.BadRequest(new { Message = "Member is not activated" }),
                    events,
                    null
                );
            }

            var bookAvailability = await bus.InvokeAsync<BookAvailabilityResponse>(
                 new GetBookAvailabilityRequest(request.BookId)
            );

            if (!bookAvailability.IsAvailable)
            {
                return (
                  Results.BadRequest(new { Message = "books is not available" }),
                  events,
                  null
              );
            }
               

            var borrowId = Guid.NewGuid();
            var borrowDate = DateTime.UtcNow;
            var dueDate = borrowDate.AddDays(14);

            var borrowingEvent = new BorrowRecordCreatedEvent(
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
            events.Add(borrowingEvent);
            outgoing.Add(
               message
            );

            return (
                Results.Created($"/api/borrowing/records/{request.BorrowId}", new { request.BorrowId }),
                events,
                outgoing
            );
        }
    }
}
