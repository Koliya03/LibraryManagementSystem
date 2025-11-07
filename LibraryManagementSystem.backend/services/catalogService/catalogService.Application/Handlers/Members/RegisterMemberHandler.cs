using Application.Commands.Books;
using Application.Commands.Members;
using Application.Interfaces;
using Domain.Books.Entities;
using Domain.Books.Events;
using Domain.Members.Entities;
using Domain.Members.Events;
using ImTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;

namespace Application.Handlers.Members
{
    public class RegisterMemberHandler
    {
        private readonly IEventStore _eventStore;

        public RegisterMemberHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Guid> Handle(RegisterMemberCommand command)
        {
            Guid id = Guid.NewGuid();
            MemberRegisteredEvent @event = Member.Register(id, command.FullName, command.Email);
            _eventStore.StartStream<Member>(id, @event);
            await _eventStore.SaveChangesAsync();
            return id;
        }
    }
}
