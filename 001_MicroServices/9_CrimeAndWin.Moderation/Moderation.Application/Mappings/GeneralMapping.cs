using AutoMapper;
using Moderation.Application.DTOs.ModerationActionDTOs;
using Moderation.Application.DTOs.ReportDTOs;
using Moderation.Domain.Entities;

namespace Moderation.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // Report
            CreateMap<CreateReportDTO, Report>();
            CreateMap<Report, ResultReportDTO>()
                .ForMember(d => d.Reason, opt => opt.MapFrom(s => s.Reason.Value)).ReverseMap();

            // ModerationAction
            CreateMap<CreateBanDTO, ModerationAction>()
                .ForMember(d => d.ActionType, opt => opt.Ignore())
                .ForMember(d => d.IsActive, opt => opt.Ignore())
                .ForMember(d => d.ActionDateUtc, opt => opt.Ignore());

            CreateMap<CreateRestrictDTO, ModerationAction>()
                .ForMember(d => d.ActionType, opt => opt.Ignore())
                .ForMember(d => d.IsActive, opt => opt.Ignore())
                .ForMember(d => d.ActionDateUtc, opt => opt.Ignore());

            CreateMap<ModerationAction, ResultModerationActionDTO>().ReverseMap();
        }
    }
}
