using Application.Interfaces;
using Domain.Entities;
using Messages.Borrowing.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Borrowing
{
    public class LateFeeIssuedHandler
    {
        private readonly ISearchIndex<Member> _members;

        public LateFeeIssuedHandler(ISearchIndex<Member> members)
        {
            _members = members;
        }

        public async Task Handle(LateFeeIssuedMessage message)
        {
            var member = await _members.GetByIdAsync(message.BorrowId);
            if (member == null)
                return;

            member.TotalLateFees += message.FeeAmount;

            await _members.UpdateAsync(member);
        }
    }
}
