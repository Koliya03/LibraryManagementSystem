using Application.Interfaces;
using Domain.Entities;
using Messages.Catalog.Events.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.catalog
{
    public class MemberSuspendedHandler
    {
        private readonly ISearchIndex<Member> _members;

        public MemberSuspendedHandler(ISearchIndex<Member> members)
        {
            _members = members;
        }

        public async Task Handle(MemberSuspendedMessage message)
        {
            var member = await _members.GetByIdAsync(message.MemberId);
            if (member == null)
                return;

            member.IsActive = false;

            await _members.UpdateAsync(member);
        }
    }
}
