using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Members.Entities;
using Wolverine.Http;

namespace Presentation.Http.Members
{
    public static class GetMembersEndpoints
    {
       
        [WolverineGet("/api/catalog/members")]
        public static async Task<IResult> GetAllMembers(IReadStore readStore, IMapper mapper)
        {
            List<Member> models = await readStore.ListAsync<Member>();
            List<MemberResponseDto> list = new List<MemberResponseDto>();

            int i = 0;
            while (i < models.Count)
            {
                MemberResponseDto dto = mapper.Map<MemberResponseDto>(models[i]);
                list.Add(dto);
                i = i + 1;
            }

            if (list.Count == 0)
                return Results.NotFound("No members found.");

            return Results.Ok(list);
        }

        [WolverineGet("/api/catalog/members/{memberId:guid}")]
        public static async Task<IResult> GetMemberById(Guid memberId, IReadStore readStore, IMapper mapper)
        {
            Member? model = await readStore.LoadAsync<Member>(memberId);
            if (model == null)
                return Results.NotFound($"Member with ID {memberId} not found.");

            MemberResponseDto dto = mapper.Map<MemberResponseDto>(model);
            return Results.Ok(dto);
        }

        [WolverineGet("/api/catalog/members/{memberId:guid}/status")]
        public static async Task<IResult> GetMemberStatus(Guid memberId, IReadStore readStore, IMapper mapper)
        {
            Member? model = await readStore.LoadAsync<Member>(memberId);
            if (model == null)
                return Results.NotFound($"Member with ID {memberId} not found.");

            MemberStatusDto dto = mapper.Map<MemberStatusDto>(model);
            return Results.Ok(dto);
        }
    }
}
