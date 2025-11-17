using Application.Commands.Members;
using Application.Interfaces;
using Domain.Members.Entities;
using Domain.Members.Events;
using Messages.Borrowing.Members;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

namespace Presentation.Http.Members
{
    public static class RegisterMemberEndpoint
    {
        [WolverinePost("/api/catalog/members/register")]
        public static (IResult, MemberRegisteredEvent, MemberRegisteredMessage) Register(
            RegisterMemberCommand command,
            [WriteAggregate(Required = false)] Member? member)
        {
            if (string.IsNullOrWhiteSpace(command.FullName))
                throw new Exception("Full name is required.");

            if (string.IsNullOrWhiteSpace(command.Email))
                throw new Exception("Email is required.");


            var evt = new MemberRegisteredEvent(
                 command.MemberId,
                 command.FullName,
                 command.Email
             );

            var msg = new MemberRegisteredMessage(
                command.MemberId,
                command.FullName,
                command.Email,
                true
            );


            return (
                Results.Created($"/api/catalog/members/{command.MemberId}", new { command.MemberId }),
                evt,
                msg
            );
        }
    }
}
