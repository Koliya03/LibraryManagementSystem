using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using ImTools;
using Messages.Borrowing;
using Wolverine;
using Wolverine.Http;

namespace Presentation.Http
{
    public static class GetBorrowRecordsEndpoints
    {
        [WolverineGet("/api/borrowing/records")]
        public static async Task<IResult> GetAll(
            IReadStore readStore,
            IMapper mapper)
        {
            var models = await readStore.ListAsync<BorrowRecord>();
            if (models.Count == 0)
                return Results.NotFound("No borrow records found.");

            var list = mapper.Map<List<BorrowRecordResponseDto>>(models);
            return Results.Ok(list);
        }

        [WolverineGet("/api/borrowing/records/{borrowId:guid}")]
        public static async Task<IResult> GetById(
            Guid borrowId,
            IReadStore readStore,
            IMapper mapper)
        {
            var model = await readStore.LoadAsync<BorrowRecord>(borrowId);
            if (model == null)
                return Results.NotFound($"Borrow record {borrowId} not found.");

            var dto = mapper.Map<BorrowRecordResponseDto>(model);
            return Results.Ok(dto);
        }

        [WolverineGet("/api/borrowing/members/{memberId:guid}/active")]
        public static async Task<IResult> GetActiveByMember(
            Guid memberId,
            IReadStore readStore,
            IMapper mapper)
        {
            var all = await readStore.ListAsync<BorrowRecord>();

            var active = all
                .Where(r => r.MemberId == memberId && !r.IsReturned && !r.IsLost)
                .ToList();

            if (active.Count == 0)
                return Results.NotFound("No active borrow records found for this member.");

            var list = mapper.Map<List<BorrowRecordResponseDto>>(active);
            return Results.Ok(list);
        }

        [WolverineGet("/api/borrowing/members/{memberId:guid}/history")]
        public static async Task<IResult> GetHistoryByMember(
            Guid memberId,
            IReadStore readStore,
            IMapper mapper)
        {
            var all = await readStore.ListAsync<BorrowRecord>();

            var history = all
                .Where(r => r.MemberId == memberId)
                .OrderByDescending(r => r.BorrowDate)
                .ToList();

            var list = mapper.Map<List<BorrowRecordResponseDto>>(history);
            return Results.Ok(list);
        }

        [WolverinePost("/api/borrowing/test/ping")]
        public static async Task<IResult> SendTestPing(
            string text,
            IMessageBus bus)
        {
            await bus.PublishAsync(new TestPing(text));
            return Results.Ok($"Sent test ping: {text}");
        }
    }
}
