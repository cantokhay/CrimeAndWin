using AutoMapper;
using Notification.Application.DTOs;
using Notification.Application.DTOs.Admin;
using Notification.Domain.VOs;

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

            CreateMap<Notification.Domain.Entities.Notification, AdminResultNotificationDTO>()
               .ForMember(d => d.Title, o => o.MapFrom(s => s.Content.Title))
               .ForMember(d => d.Message, o => o.MapFrom(s => s.Content.Message))
               .ForMember(d => d.Type, o => o.MapFrom(s => s.Content.Type))
               .ReverseMap();

            CreateMap<AdminCreateNotificationDTO, Notification.Domain.Entities.Notification>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Content,
                    o => o.MapFrom(s => new NotificationContent(s.Title, s.Message, s.Type)))
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.UpdatedAtUtc, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore());

            CreateMap<AdminUpdateNotificationDTO, Notification.Domain.Entities.Notification>()
                .ForMember(d => d.Content,
                    o => o.MapFrom(s => new NotificationContent(s.Title, s.Message, s.Type)))
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore());
        }
    }
}
