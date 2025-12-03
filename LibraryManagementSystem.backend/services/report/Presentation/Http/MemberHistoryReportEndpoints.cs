using Application.Services;
using Wolverine.Http;

namespace Presentation.Http
{
    public static class MemberHistoryReportEndpoints
    {
        [WolverineGet("/api/reporting/members/{memberId:guid}/history")]
        public static async Task<IResult> GetHistory(
            Guid memberId,
            MemberHistoryReportService service)
        {
            var result = await service.GetMemberHistoryAsync(memberId);


            if (result == null || result.Count == 0)
                return Results.NotFound("No history found for this member.");

            return Results.Ok(new {list = result });
        }
    }
}
