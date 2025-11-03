using Application.Interfaces;
using Application.Queries.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Members
{
    public class GetMemberActiveStatusHandler
    {
        private readonly IMemberRepository _repository;

        public GetMemberActiveStatusHandler(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(GetMemberActiveStatusQuery query)
        {
            var member = await _repository.GetByIdAsync(query.MemberId);       
            return member.IsActive; 
        }
    }
}
