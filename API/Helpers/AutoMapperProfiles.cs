using System;
using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => 
                    src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => 
                    src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<MessageDto, Message>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Credit, CreditDto>();
            CreateMap<CreditPayItem, CreditPayItemDto>();

            //  CreateMap<Credit, CreditDto>()
            //      .ForMember(dest => dest.GTotal, opt => opt.MapFrom(src 
            //      => src.GTotal.HasValue ? src.GTotal.Value.ToString() : string.Empty));
        
            // CreateMap<CreditDto, Credit>()
            //      .ForMember(dest => dest.GTotal, opt => opt.MapFrom(src 
            //      => src.GTotal.HasValue ? src.GTotal.Value.ToString() : string.Empty));

        }
    }
}