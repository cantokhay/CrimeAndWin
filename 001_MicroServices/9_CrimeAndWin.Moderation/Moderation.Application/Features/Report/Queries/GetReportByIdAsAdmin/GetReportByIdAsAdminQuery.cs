using MediatR;
using Moderation.Application.DTOs.ReportDTOs.Admin;

namespace Moderation.Application.Features.Report.Queries.GetReportByIdAsAdmin
{
    public sealed record GetReportByIdAsAdminQuery(Guid id) : IRequest<AdminResultReportDTO?>;
}
