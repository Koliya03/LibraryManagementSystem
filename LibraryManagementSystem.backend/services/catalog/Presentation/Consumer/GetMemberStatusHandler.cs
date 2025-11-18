using Application.DTO;
using Application.Interfaces;
using Domain.Members.Entities;
using Messages.Borrowing.Requests;
using Messages.Catalog.Responses;
using Wolverine.Attributes;

namespace Presentation.Consumer
{
    public static class GetMemberStatusHandler
    {
        [MessageTimeout(2)]
        public static async Task<MemberStatusResponse> Handle(
            GetMemberStatusRequest message,
            IReadStore readStore)
        {
            var member = await readStore.LoadAsync<Member>(message.MemberId);

            if (member == null)
            {
                return new MemberStatusResponse
                (
                     message.MemberId,
                     false
                );
            }

            return new MemberStatusResponse
            (
                member.Id,
                member.IsActive
            );
        }
    }
}
