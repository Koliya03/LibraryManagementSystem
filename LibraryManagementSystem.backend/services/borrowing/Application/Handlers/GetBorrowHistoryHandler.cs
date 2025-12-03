using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Messages.Borrowing.Response;
using Messages.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Application.Handlers
{
    public static class GetBorrowHistoryHandler
    {
        [MessageTimeout(2)]
        public static async Task<BorrowHistoryResponse> Handle(
            GetBorrowHistoryRequest message,
            IReadStore readStore)
        {

            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" borrowing RECEIVED  BORROW HINSTORY from report:");
            Console.ResetColor();

            var all = await readStore.ListAsync<BorrowRecord>();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"all borrowing records count: {all.Count()}");
            Console.WriteLine($"all borrowing records count: {all}");
            Console.ResetColor();

            var history = all
                .Where(r => r.MemberId == message.MemberId)
                .Select(r => new BorrowRecordResponseDto(
                    r.Id,
                    r.MemberId,
                    r.BookId,
                    r.BorrowDate,
                    r.DueDate,
                    r.ReturnDate,
                    r.IsReturned,
                    r.IsLost,
                    r.LateFee
                ))
                .ToList();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($" borrowing records count for the member ID: {history.Count()}");
            Console.ResetColor();

            return new BorrowHistoryResponse(history);
        }
    }
}
