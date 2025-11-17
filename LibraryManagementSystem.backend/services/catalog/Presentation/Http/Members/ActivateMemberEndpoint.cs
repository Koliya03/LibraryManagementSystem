using Application.Interfaces;
using Domain.Members.Entities;
using Domain.Members.Events;
using Messages.Borrowing.Members;
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
        public static (IResult, MemberActivatedEvent, MemberActivatedMessage) Activate(
            Guid memberId,
            [WriteAggregate(nameof(memberId))] Member member)
        {
            var evt = new MemberActivatedEvent(memberId);
            var msg = new MemberActivatedMessage(memberId);

            return (
                Results.Ok(new { Message = "Member activated successfully", memberId }),
                evt,
                msg
            );
        }
    }
}
