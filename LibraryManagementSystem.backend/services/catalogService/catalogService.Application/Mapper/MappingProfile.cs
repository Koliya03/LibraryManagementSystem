using Application.DTO;
using Application.ReadModels;
using AutoMapper;
using Domain.Books.Entities;
using Domain.Members.Entities;
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
           
            CreateMap<BookReadModel, BookResponseDto>();
            CreateMap<MemberReadModel, MemberResponseDto>();

            
            CreateMap<BookReadModel, AvailabilityDto>()
                .ForMember(d => d.BookId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.AvailableQuantity, o => o.MapFrom(s => s.AvailableQuantity))
                .ForMember(d => d.IsAvailable, o => o.MapFrom(s => s.AvailableQuantity > 0 && !s.IsRetired));

            
            CreateMap<MemberReadModel, MemberStatusDto>()
                .ForMember(d => d.MemberId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive));
        }
    }
}
