using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Wolverine.Http;

namespace Presentation.Http
{
    public static class BookSearchEndpoints
    {
        [WolverineGet("/api/search/books")]
        public static async Task<IResult> SearchBooks(
            string search,
            ISearchIndex<Book> books,
            IMapper mapper)
        {
            if (string.IsNullOrWhiteSpace(search))
                return Results.BadRequest(new { Message = "search is required." });

            var results = await books.SearchAsync(search);

            var dtoList = mapper.Map<List<BookSearchResponseDto>>(results);

            return Results.Ok(new { dtoList });
        }

        [WolverineGet("/api/search/books/{bookId:guid}")]
        public static async Task<IResult> GetById(
            Guid bookId,
            ISearchIndex<Book> books)
        {
            var book = await books.GetByIdAsync(bookId);

            if (book == null)
            {
                return Results.BadRequest(new { Message = $"Book with ID {bookId} not found." });
            }

            return Results.Ok(new { book });
        }
    }
}
