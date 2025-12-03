using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookSearchResponseDto>();
            CreateMap<Book, BookSearchResponseDto>()
           .ForMember(d => d.BookId,
               o => o.MapFrom(s => s.Id));

            CreateMap<Member, MemberSearchResponseDto>();
            CreateMap<Member, MemberSearchResponseDto>()
                .ForMember(d => d.MemberId,
               o => o.MapFrom(s => s.Id));
        }
    }
}
