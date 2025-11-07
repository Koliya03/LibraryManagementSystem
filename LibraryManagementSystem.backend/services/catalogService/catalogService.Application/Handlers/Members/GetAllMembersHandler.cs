using Application.DTO;
using Application.Interfaces;
using Application.Queries.Members;
using Application.ReadModels;
using AutoMapper;
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
        private readonly IReadStore _readStore;
        private readonly IMapper _mapper;

        public GetAllMembersHandler(IReadStore readStore, IMapper mapper)
        {
            _readStore = readStore;
            _mapper = mapper;
        }

        public async Task<List<MemberResponseDto>> Handle()
        {
            List<MemberReadModel> models = await _readStore.ListAsync<MemberReadModel>();
            List<MemberResponseDto> list = new List<MemberResponseDto>();
            int i = 0;
            while (i < models.Count)
            {
                MemberResponseDto dto = _mapper.Map<MemberResponseDto>(models[i]);
                list.Add(dto);
                i = i + 1;
            }
            return list;
        }
    }
}
