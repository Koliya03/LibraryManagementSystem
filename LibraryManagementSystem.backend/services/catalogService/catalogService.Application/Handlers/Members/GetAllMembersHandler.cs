using Application.Interfaces;
using Application.Queries.Members;
using Domain.Members.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Members
{
    public class GetAllMembersHandler
    {
        private readonly IMemberRepository _repository;

        public GetAllMembersHandler(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Member>> Handle(GetAllMembersQuery query)
        {
            return await _repository.GetAllAsync();
        }
    }
}
