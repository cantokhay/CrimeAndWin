using MediatR;
using Moderation.Application.DTOs.ReportDTOs.Admin;

namespace Moderation.Application.Features.Report.Commands.AdminCreateReport
{
    public sealed record AdminCreateReportCommand(AdminCreateReportDTO createReportDTO) : IRequest<Guid>;
}
