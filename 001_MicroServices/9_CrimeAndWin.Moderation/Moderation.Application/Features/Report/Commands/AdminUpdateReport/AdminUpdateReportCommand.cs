using Shared.Application.Abstractions.Messaging;
using Moderation.Application.DTOs.ReportDTOs.Admin;

namespace Moderation.Application.Features.Report.Commands.AdminUpdateReport
{
    public sealed record AdminUpdateReportCommand(AdminUpdateReportDTO updateReportDTO) : IRequest<bool>;
}


