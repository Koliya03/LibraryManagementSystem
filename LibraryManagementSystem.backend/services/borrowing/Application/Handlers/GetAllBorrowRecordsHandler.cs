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
    public static class GetAllBorrowRecordsHandler
    {
        [MessageTimeout(2)]
        public static async Task<AllBorrowRecordsResponse> Handle(
            GetAllBorrowRecordsRequest message,
            IReadStore readStore)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" borrowing RECEIVED ALL BORROW from report:");
            Console.ResetColor();

            var allRecords = await readStore.ListAsync<BorrowRecord>();

            var list = new List<BorrowRecordResponseDto>();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" borrowing RECEIVED ALL BORROW from report:");
            Console.ResetColor();

            foreach (var r in allRecords)
            {
                list.Add(new BorrowRecordResponseDto(
                    r.Id,
                    r.MemberId,
                    r.BookId,
                    r.BorrowDate,
                    r.DueDate,
                    r.ReturnDate,
                    r.IsReturned,
                    r.IsLost,
                    r.LateFee
                ));
            }

            return new AllBorrowRecordsResponse(list);
        }
    }
}