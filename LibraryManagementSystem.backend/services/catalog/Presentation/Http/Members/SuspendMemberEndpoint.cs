using Application.Interfaces;
using Domain.Member.Events;
using Domain.Members.Entities;
using Messages.Borrowing.Members;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Members
{
    public static class SuspendMemberEndpoint
    {
        [WolverinePut("/api/catalog/members/{memberId:guid}/suspend")]
        public static (IResult, MemberSuspendedEvent, MemberSuspendedMessage) Suspend(
            Guid memberId,
            [WriteAggregate(nameof(memberId))] Member member)
        {
            var evt = new MemberSuspendedEvent(memberId);
            var msg = new MemberSuspendedMessage(memberId);

            return (
                Results.Ok(new { Message = "Member suspended successfully", memberId }),
                evt,
                msg
            );
        }
    }
}
