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
    public class MemberActivatedHandler
    {
        private readonly ISearchIndex<Member> _members;

        public MemberActivatedHandler(ISearchIndex<Member> members)
        {
            _members = members;
        }

        public async Task Handle(MemberActivatedMessage message)
        {
            var member = await _members.GetByIdAsync(message.MemberId);
            if (member == null)
                return;

            member.IsActive = true;

            await _members.UpdateAsync(member);
        }
    }
}
