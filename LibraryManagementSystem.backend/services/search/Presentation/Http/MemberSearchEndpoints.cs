using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Wolverine.Http;

namespace Presentation.Http
{
    public static class MemberSearchEndpoints
    {
        [WolverineGet("/api/search/members")]
        public static async Task<IResult> SearchMembers(
            string search,
            ISearchIndex<Member> members,
            IMapper mapper)
        {
            if (string.IsNullOrWhiteSpace(search))
                return Results.BadRequest(new { Message = "search is required." });

            var results = await members.SearchAsync(search);

            var dtoList = mapper.Map<List<MemberSearchResponseDto>>(results);

            return Results.Ok(new { dtoList });
        }
    }
}
