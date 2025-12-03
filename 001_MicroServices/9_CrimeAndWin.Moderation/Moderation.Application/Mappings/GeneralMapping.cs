using AutoMapper;
using Moderation.Application.DTOs.ModerationActionDTOs;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;
using Moderation.Application.DTOs.ReportDTOs;
using Moderation.Application.DTOs.ReportDTOs.Admin;
using Moderation.Domain.Entities;
using Moderation.Domain.VOs;

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

            // ========================
            //        ADMIN MAPS
            // ========================

            // Report – Admin
            CreateMap<Report, AdminResultReportDTO>()
                .ForMember(d => d.Reason, o => o.MapFrom(s => s.Reason.Value))
                .ReverseMap();

            CreateMap<AdminCreateReportDTO, Report>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Reason, o => o.MapFrom(s => new ReportReason(s.Reason)))
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.UpdatedAtUtc, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore());

            CreateMap<AdminUpdateReportDTO, Report>()
                .ForMember(d => d.Reason, o => o.MapFrom(s => new ReportReason(s.Reason)))
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore());

            // ModerationAction – Admin
            CreateMap<ModerationAction, AdminResultModerationActionDTO>().ReverseMap();

            CreateMap<AdminCreateModerationActionDTO, ModerationAction>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.UpdatedAtUtc, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore());

            CreateMap<AdminUpdateModerationActionDTO, ModerationAction>()
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore());
        }
    }
}
