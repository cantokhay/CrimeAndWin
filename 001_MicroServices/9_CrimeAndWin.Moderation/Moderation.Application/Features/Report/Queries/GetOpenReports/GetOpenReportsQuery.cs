using Shared.Application.Abstractions.Messaging;
using Moderation.Application.DTOs.ReportDTOs;

namespace Moderation.Application.Features.Report.Queries.GetOpenReports
{
    public record GetOpenReportsQuery() : IRequest<List<ResultReportDTO>>;
}


