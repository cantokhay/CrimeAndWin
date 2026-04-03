using Moderation.Application.DTOs.ModerationActionDTOs;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;
using Moderation.Application.DTOs.ReportDTOs;
using Moderation.Application.DTOs.ReportDTOs.Admin;
using Moderation.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Moderation.Application.Mapping;

[Mapper]
public partial class ModerationMapper
{
    // Report
    public partial Report ToEntity(CreateReportDTO dto);
    
    [MapProperty(nameof(Report.Reason.Value), nameof(ResultReportDTO.Reason))]
    public partial ResultReportDTO ToResultDto(Report entity);
    
    public partial Report ToEntity(ResultReportDTO dto);

    // ModerationAction
    [MapperIgnoreTarget(nameof(ModerationAction.ActionType))]
    [MapperIgnoreTarget(nameof(ModerationAction.IsActive))]
    [MapperIgnoreTarget(nameof(ModerationAction.ActionDateUtc))]
    public partial ModerationAction ToEntity(CreateBanDTO dto);

    [MapperIgnoreTarget(nameof(ModerationAction.ActionType))]
    [MapperIgnoreTarget(nameof(ModerationAction.IsActive))]
    [MapperIgnoreTarget(nameof(ModerationAction.ActionDateUtc))]
    public partial ModerationAction ToEntity(CreateRestrictDTO dto);

    public partial ResultModerationActionDTO ToResultDto(ModerationAction entity);
    public partial ModerationAction ToEntity(ResultModerationActionDTO dto);

    // List Mappings
    public partial IEnumerable<ResultReportDTO> ToResultDtoList(IEnumerable<Report> entities);
    public partial IEnumerable<ResultModerationActionDTO> ToResultDtoList(IEnumerable<ModerationAction> entities);

    // ========================
    //        ADMIN MAPS
    // ========================

    [MapProperty(nameof(Report.Reason.Value), nameof(AdminResultReportDTO.Reason))]
    public partial AdminResultReportDTO ToAdminResultDto(Report entity);
    public partial Report ToEntity(AdminResultReportDTO dto);

    [MapperIgnoreTarget(nameof(Report.Id))]
    [MapperIgnoreTarget(nameof(Report.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(Report.UpdatedAtUtc))]
    [MapperIgnoreTarget(nameof(Report.IsDeleted))]
    [MapProperty(nameof(AdminCreateReportDTO.Reason), nameof(Report.Reason))]
    public partial Report ToEntity(AdminCreateReportDTO dto);

    [MapperIgnoreTarget(nameof(Report.CreatedAtUtc))]
    [MapProperty(nameof(AdminUpdateReportDTO.Reason), nameof(Report.Reason))]
    public partial Report ToEntity(AdminUpdateReportDTO dto);

    public partial AdminResultModerationActionDTO ToAdminResultDto(ModerationAction entity);
    public partial ModerationAction ToEntity(AdminResultModerationActionDTO dto);

    [MapperIgnoreTarget(nameof(ModerationAction.Id))]
    [MapperIgnoreTarget(nameof(ModerationAction.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(ModerationAction.UpdatedAtUtc))]
    [MapperIgnoreTarget(nameof(ModerationAction.IsDeleted))]
    public partial ModerationAction ToEntity(AdminCreateModerationActionDTO dto);

    [MapperIgnoreTarget(nameof(ModerationAction.CreatedAtUtc))]
    public partial ModerationAction ToEntity(AdminUpdateModerationActionDTO dto);

    // Admin List Mappings
    public partial IEnumerable<AdminResultReportDTO> ToAdminResultDtoList(IEnumerable<Report> entities);
    public partial IEnumerable<AdminResultModerationActionDTO> ToAdminResultDtoList(IEnumerable<ModerationAction> entities);
}


