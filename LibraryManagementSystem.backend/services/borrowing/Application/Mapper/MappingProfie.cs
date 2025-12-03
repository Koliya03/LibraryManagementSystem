using AutoMapper;
using Domain.Entities;
using Messages.Borrowing.Response;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BorrowRecord, BorrowRecordResponseDto>();
        }
    }
}

