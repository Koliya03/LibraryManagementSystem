using Application.DTO;
using Application.Interfaces;
using Application.Queries.Book;
using Application.ReadModels;
using AutoMapper;
using Domain.Books.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;

namespace Application.Handlers.Books
{
    public class GetBookByIdHandler
    {
        private readonly IReadStore _readStore;
        private readonly IMapper _mapper;

        public GetBookByIdHandler(IReadStore readStore, IMapper mapper)
        {
            _readStore = readStore;
            _mapper = mapper;
        }

        public BookResponseDto Handle(GetBookByIdQuery query)
        {
            BookReadModel model = _readStore.LoadAsync<BookReadModel>(query.BookId).GetAwaiter().GetResult();
            if (model == null) throw new Exception("Book not found.");

            BookResponseDto dto = _mapper.Map<BookResponseDto>(model);
            return dto;
        }
    }
}
