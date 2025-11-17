using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Books.Entities;
using Wolverine.Http;

namespace Presentation.Http.Books
{
    public static class GetBooksEndpoints
    {
       
        [WolverineGet("/api/catalog/books")]
        public static async Task<IResult> GetAllBooks(IReadStore readStore, IMapper mapper)
        {
            var models = await readStore.ListAsync<Book>();

            if (models == null || models.Count == 0)
                return Results.NotFound("No books found.");

            var list = new List<BookResponseDto>();

            foreach (var model in models)
            {
                var dto = mapper.Map<BookResponseDto>(model);
                list.Add(dto);
            }

            return Results.Ok(list);
        }

        [WolverineGet("/api/catalog/books/{bookId:guid}")]
        public static async Task<IResult> GetBookById(Guid bookId, IReadStore readStore, IMapper mapper)
        {
            var model = await readStore.LoadAsync<Book>(bookId);

            if (model == null)
                return Results.NotFound($"Book with ID {bookId} not found.");

            var dto = mapper.Map<BookResponseDto>(model);
            return Results.Ok(dto);
        }

        [WolverineGet("/api/catalog/books/{bookId:guid}/availability")]
        public static async Task<IResult> GetAvailability(Guid bookId, IReadStore readStore, IMapper mapper)
        {
            var model = await readStore.LoadAsync<Book>(bookId);

            if (model == null)
                return Results.NotFound($"Book with ID {bookId} not found.");

            var dto = mapper.Map<AvailabilityDto>(model);
            return Results.Ok(dto);
        }
    }
}
