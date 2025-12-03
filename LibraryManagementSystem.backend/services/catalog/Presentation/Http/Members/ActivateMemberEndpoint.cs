using Application.Interfaces;
using Domain.Members.Entities;
using Domain.Members.Events;
using Messages.Catalog.Events.Members;
using Microsoft.AspNetCore.Http.HttpResults;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence;

namespace Presentation.Http.Members
{
    public static class ActivateMemberEndpoint
    {
        [WolverinePut("/api/catalog/members/{memberId:guid}/activate")]
        public static (IResult, Events, OutgoingMessages) Activate(
            Guid memberId,
            [WriteAggregate(nameof(memberId))] Member member)
        {
            var events = new Events();
            var outgoing = new OutgoingMessages();

            var evt = new MemberActivatedEvent(memberId);
            events.Add(evt);

            var msg = new MemberActivatedMessage(memberId);
            outgoing.Add(msg);

            return (
                Results.Ok(new { Message = "Member activated successfully", memberId }),
                events,
                outgoing
            );
        }
    }
}
