using Application.Commands.Members;
using Application.Interfaces;
using Domain.Members.Entities;
using Domain.Members.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Members
{
    public class ActivateMemberHandler
    {
        private readonly IEventStore _eventStore;

        public ActivateMemberHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Handle(ActivateMemberCommand command)
        {
            Member aggregate = await _eventStore.AggregateAsync<Member>(command.MemberId);
            if (aggregate == null) throw new Exception("Member not found.");

            MemberActivatedEvent @event = aggregate.Activate();
            _eventStore.Append(command.MemberId, @event);
            await _eventStore.SaveChangesAsync();
        }
    }
}
