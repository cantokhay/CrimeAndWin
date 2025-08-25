using AutoMapper;
using Notification.Application.DTOs;

namespace Notification.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Domain.Entities.Notification, ResultNotificationDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Content.Title))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Content.Message))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Content.Type))
                .ReverseMap();

            CreateMap<Domain.Entities.Notification, CreateNotificationDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Content.Title))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Content.Message))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Content.Type))
                .ReverseMap();
            
            CreateMap<Domain.Entities.Notification, UpdateNotificationDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Content.Title))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Content.Message))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Content.Type))
                .ReverseMap();
            
            CreateMap<Domain.Entities.Notification, GetByIdNotificationDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Content.Title))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Content.Message))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Content.Type))
                .ReverseMap();
        }
    }
}
