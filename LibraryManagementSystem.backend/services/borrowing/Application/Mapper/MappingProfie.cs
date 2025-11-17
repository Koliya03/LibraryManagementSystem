using Application.DTO;
using AutoMapper;
using Domain.Entities;

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

