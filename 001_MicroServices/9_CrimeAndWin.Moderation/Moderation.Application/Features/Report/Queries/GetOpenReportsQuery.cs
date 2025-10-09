using MediatR;
using Moderation.Application.DTOs.ReportDTOs;

namespace Moderation.Application.Features.Report.Queries
{
    public record GetOpenReportsQuery() : IRequest<List<ResultReportDTO>>;
}
