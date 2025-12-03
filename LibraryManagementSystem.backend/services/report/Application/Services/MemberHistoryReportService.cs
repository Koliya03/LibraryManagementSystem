using Application.DTOs;
using Messages.Borrowing.Response;
using Messages.Catalog.Responses;
using Messages.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;

namespace Application.Services
{
    public class MemberHistoryReportService
    {
        private readonly IMessageContext _bus;

        public MemberHistoryReportService(IMessageContext bus)
        {
            _bus = bus;
        }

        public async Task<List<BorrowHistoryItemDto>> GetMemberHistoryAsync(Guid memberId)
        {
            var response = await _bus.InvokeAsync<BorrowHistoryResponse>(
                new GetBorrowHistoryRequest(memberId));

            var history = response?.Records ?? new List<BorrowRecordResponseDto>();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($" borrowing records count: {history.Count()}");
            Console.WriteLine($" borrowing records :{history}");
            Console.ResetColor();

            if (history == null || history.Count == 0)
                return new List<BorrowHistoryItemDto>();

            var result = new List<BorrowHistoryItemDto>();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" number of items: ");
            Console.ResetColor();

            foreach (var record in history)
            {
                var book = await _bus.InvokeAsync<BookResponseDto>(
                    new GetBookByIdRequest(record.BookId)
                );

                var dto = new BorrowHistoryItemDto
                {
                    BorrowId = record.Id,
                    MemberId = record.MemberId,
                    BookId = record.BookId,

                    BorrowDate = record.BorrowDate,
                    DueDate = record.DueDate,
                    ReturnDate = record.ReturnDate,
                    IsReturned = record.IsReturned,
                    IsLost = record.IsLost,
                    LateFee = record.LateFee,

                    Title = book?.Title ?? "",
                    Author = book?.Author ?? ""
                };

                result.Add(dto);
            }

            return result;
        }
    }
}
