using Application.DTO;
using Application.Interfaces;
using Application.Queries.Members;
using Application.ReadModels;
using AutoMapper;
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
        private readonly IReadStore _readStore;
        private readonly IMapper _mapper;

        public GetMemberByIdHandler(IReadStore readStore, IMapper mapper)
        {
            _readStore = readStore;
            _mapper = mapper;
        }

        public async Task<MemberResponseDto?> Handle(Guid memberId)
        {
            MemberReadModel model = await _readStore.LoadAsync<MemberReadModel>(memberId);
            if (model == null) return null;
            MemberResponseDto dto = _mapper.Map<MemberResponseDto>(model);
            return dto;
        }
    }
}
