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
    public class TopBorrowedBooksReportService
    {
        private readonly IMessageContext _bus;

        public TopBorrowedBooksReportService(IMessageContext bus)
        {
            _bus = bus;
        }

        public async Task<List<BorrowedBookDto>> GetTopBorrowedBooksAsync(int top = 10)
        {
            var response = await _bus.InvokeAsync<AllBorrowRecordsResponse>(
                new GetAllBorrowRecordsRequest()
            );

            var all = response.Records;

            if (all == null || all.Count == 0)
                return new List<BorrowedBookDto>();

            var grouped = all
                .GroupBy(b => b.BookId)
                .Select(g => new { BookId = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(top)
                .ToList();

            var result = new List<BorrowedBookDto>();

            foreach (var item in grouped)
            {
                var book = await _bus.InvokeAsync<BookResponseDto>(
                    new GetBookByIdRequest(item.BookId)
                );

                result.Add(new BorrowedBookDto
                {
                    BookId = item.BookId,
                    TimesBorrowed = item.Count,
                    Title = book?.Title ?? "",
                    Author = book?.Author ?? "",
                    ISBN = book?.ISBN ?? ""
                });
            }

            return result;
        }

    }
}
