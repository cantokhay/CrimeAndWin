using MediatR;
using Moderation.Application.DTOs.ReportDTOs.Admin;

namespace Moderation.Application.Features.Report.Queries.GetAllReportsAsAdmin
{
    public sealed record GetAllReportsAsAdminQuery() : IRequest<List<AdminResultReportDTO>>;
}
