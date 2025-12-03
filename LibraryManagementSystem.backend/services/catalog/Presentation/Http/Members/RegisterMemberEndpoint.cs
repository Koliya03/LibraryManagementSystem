using Application.DTO;
using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Members.Entities;
using Domain.Members.Events;
using Messages.Catalog.Events.Members;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Members
{
    public static class RegisterMemberEndpoint
    {
        [WolverinePost("/api/catalog/members/register")]
        public static (IResult, Events, OutgoingMessages) Register(
            RegisterMemberDto registerMember,
            [WriteAggregate(Required = false)] Member member)
        {

            var events = new Events();
            var outgoing = new OutgoingMessages();
            if (member != null)
            {
                return (
                    Results.BadRequest(new { Message = $"Member with id {member.Id} already exists." }),
                    events,
                    outgoing
                );
            }

            if (string.IsNullOrWhiteSpace(registerMember.FullName))
                return (Results.BadRequest(new { Message = "Full name is required." }), events, outgoing);

            if (string.IsNullOrWhiteSpace(registerMember.Email))
                return (Results.BadRequest(new { Message = "Email is required." }), events, outgoing);


            var evt = new MemberRegisteredEvent(
                 registerMember.MemberId,
                 registerMember.FullName,
                 registerMember.Email
            );
            events.Add(evt);

            var msg = new MemberRegisteredMessage(
                registerMember.MemberId,
                registerMember.FullName,
                registerMember.Email,
                true
            );
            outgoing.Add(msg);


            return (
                Results.Created($"/api/catalog/members/{registerMember.MemberId}", new { registerMember.MemberId }),
                events,
                outgoing
            );
        }
    }
}
