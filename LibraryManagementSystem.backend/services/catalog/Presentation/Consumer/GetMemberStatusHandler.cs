using Application.DTO;
using Application.Interfaces;
using Domain.Members.Entities;
using Messages.Borrowing.Requests;
using Wolverine.Attributes;

namespace Presentation.Consumer
{
    public static class GetMemberStatusHandler
    {
        [MessageTimeout(1)]
        public static async Task<MemberStatusDto> Handle(
            GetMemberStatusRequest message,
            IReadStore readStore)
        {
            var member = await readStore.LoadAsync<Member>(message.MemberId);

            if (member == null)
            {
                return new MemberStatusDto
                {
                    MemberId = message.MemberId,
                    IsActive = false
                };
            }

            return new MemberStatusDto
            {
                MemberId = member.Id,
                IsActive = member.IsActive
            };
        }
    }
}
