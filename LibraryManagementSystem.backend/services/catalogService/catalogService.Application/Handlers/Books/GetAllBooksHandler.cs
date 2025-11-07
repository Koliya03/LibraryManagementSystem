using Application.DTO;
using Application.Interfaces;
using Application.Queries.Book;
using Application.ReadModels;
using AutoMapper;
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
        private readonly IReadStore _readStore;
        private readonly IMapper _mapper;

        public GetAllBooksHandler(IReadStore readStore, IMapper mapper)
        {
            _readStore = readStore;
            _mapper = mapper;
        }

        public List<BookResponseDto> Handle(GetAllBooksQuery query)
        {
            List<BookReadModel> models = _readStore.ListAsync<BookReadModel>().GetAwaiter().GetResult();
            List<BookResponseDto> list = new List<BookResponseDto>();

            if (models != null)
            {
                int i = 0;
                int count = models.Count;
                while (i < count)
                {
                    BookResponseDto dto = _mapper.Map<BookResponseDto>(models[i]);
                    list.Add(dto);
                    i = i + 1;
                }
            }

            return list;
        }
    }
}

