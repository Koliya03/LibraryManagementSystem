using Application.Interfaces;
using Application.Queries.Book;
using Domain.Books.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Configuration;

namespace Application.Handlers.Books
{
    public class GetAllBooksHandler
    {
        private readonly IBookRepository _repository;

        public GetAllBooksHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Book>> Handle(GetAllBooksQuery query)
        {
            return await _repository.GetAllAsync();
        }
    }
}
