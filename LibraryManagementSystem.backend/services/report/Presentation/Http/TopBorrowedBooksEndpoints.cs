using Application.Services;
using Wolverine.Http;

namespace Presentation.Http
{
    public static class TopBorrowedBooksEndpoints
    {
        [WolverineGet("/api/reporting/books/top")]
        public static async Task<IResult> GetTopBooks(
            int count,
            TopBorrowedBooksReportService service)
        {
            if (count <= 0)
                count = 10;

            var result = await service.GetTopBorrowedBooksAsync(count);

            if (result == null || result.Count == 0)
                return Results.NotFound("No borrowed books found.");

            return Results.Ok(new {list = result});
        }
    }
}
