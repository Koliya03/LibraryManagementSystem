using Application.Interfaces;
using Application.Queries.Book;
using Domain.Books.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;

namespace Application.Handlers.Books
{
    public class GetBooksByIdHandler
    {
        private readonly IBookRepository _repository;

        public GetBooksByIdHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<Book> Handle(GetBookByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.BookId);
        }
    }
}
