using Application.Commands.Members;
using Application.Interfaces;
using Domain.Member.Events;
using Domain.Members.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Members
{
    public class SuspendMemberHandler
    {
        private readonly IEventStore _eventStore;

        public SuspendMemberHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Handle(SuspendMemberCommand command)
        {
            Member aggregate = await _eventStore.AggregateAsync<Member>(command.MemberId);
            if (aggregate == null) throw new Exception("Member not found.");

            MemberSuspendedEvent @event = aggregate.Suspend();
            _eventStore.Append(command.MemberId, @event);
            await _eventStore.SaveChangesAsync();
        }
    }
}
