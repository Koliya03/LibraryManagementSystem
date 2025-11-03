using Application.Commands.Members;
using Application.Interfaces;
using Domain.Member.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;

namespace Application.Handlers.Members
{
    public class UpdateMemberStatusHandler
    {
        private readonly IMemberRepository _repository;

        public UpdateMemberStatusHandler(IMemberRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateMemberStatusCommand command, IMessageContext context)
        {
            var member = await _repository.GetByIdAsync(command.Id);
            if (member == null)
            {
                Console.WriteLine("member cannot be found");
                return;
            }

            if (member.IsActive != command.ActiveStatus)
            {
                member.IsActive = command.ActiveStatus;
            }
            _repository.UpdateMemberAsync(member);

            await context.PublishAsync(new MemberStatusChangedEvent(member.Id, member.IsActive));

        }

    }
}
