using Application.DTO;
using Application.Interfaces;
using Application.Queries.Members;
using Application.ReadModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Members
{
    public class GetMemberActiveStatusHandler
    {
        private readonly IReadStore _readStore;
        private readonly IMapper _mapper;

        public GetMemberActiveStatusHandler(IReadStore readStore, IMapper mapper)
        {
            _readStore = readStore;
            _mapper = mapper;
        }

        public async Task<MemberStatusDto?> Handle(GetMemberActiveStatusQuery query)
        {
            MemberReadModel model = await _readStore.LoadAsync<MemberReadModel>(query.MemberId);
            if (model == null) return null;
            MemberStatusDto dto = _mapper.Map<MemberStatusDto>(model);
            return dto;
        }
    }
}
