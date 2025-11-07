using Application.DTO;
using Application.Interfaces;
using Application.Queries.Books;
using Application.ReadModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Books
{
    public class GetBookAvailabilityHandler
    {
        private readonly IReadStore _readStore;
        private readonly IMapper _mapper;

        public GetBookAvailabilityHandler(IReadStore readStore, IMapper mapper)
        {
            _readStore = readStore;
            _mapper = mapper;
        }

        public AvailabilityDto Handle(GetBookAvailabilityQuery query)
        {
            BookReadModel model = _readStore.LoadAsync<BookReadModel>(query.BookId).GetAwaiter().GetResult();
            if (model == null) throw new Exception("Book not found.");


            AvailabilityDto dto = _mapper.Map<AvailabilityDto>(model);
            return dto;
        }
    }
}
