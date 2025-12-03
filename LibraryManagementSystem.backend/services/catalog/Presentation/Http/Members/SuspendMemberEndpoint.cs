using Application.Interfaces;
using Domain.Member.Events;
using Domain.Members.Entities;
using Messages.Catalog.Events.Members;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Members
{
    public static class SuspendMemberEndpoint
    {
        [WolverinePut("/api/catalog/members/{memberId:guid}/suspend")]
        public static (IResult, Events, OutgoingMessages) Suspend(
            Guid memberId,
            [WriteAggregate(nameof(memberId))] Member member)
        {
            var events = new Events();
            var outgoing = new OutgoingMessages();

            var evt = new MemberSuspendedEvent(memberId);
            events.Add(evt);

            var msg = new MemberSuspendedMessage(memberId);
            outgoing.Add(msg);

            return (
                Results.Ok(new { Message = "Member suspended successfully", memberId }),
                events,
                outgoing
            );
        }
    }
}
