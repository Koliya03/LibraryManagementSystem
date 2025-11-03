using Application.Interfaces;
using Application.Queries.Members;
using Domain.Members.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Members
{
    public class GetMemberByIdHandler
    {
        private readonly IMemberRepository _repository;

        public GetMemberByIdHandler(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<Member> Handle(GetMemberByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.MemberId);
        }
    }
}
