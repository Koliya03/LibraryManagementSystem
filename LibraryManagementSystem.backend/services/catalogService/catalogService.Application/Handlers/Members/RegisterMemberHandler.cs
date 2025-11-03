using Application.Commands.Members;
using Application.Interfaces;
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
        private readonly IMemberRepository _repository;

        public RegisterMemberHandler(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RegisterMemberCommand command,IMessageContext context)
        {
            var member = new Member
            {
                Id = Guid.NewGuid(),
                FullName = command.FullName,
                Email = command.Email,
                IsActive = true
            };

            _repository.AddMemberAsync(member);
            await context.PublishAsync(new MemberRegisteredEvent(member.Id, member.FullName, member.Email));
        }
    }
}
