using MediatR;
using Moderation.Application.DTOs.ReportDTOs;

namespace Moderation.Application.Features.ModerationAction.Queries.GetAllReports
{
    public sealed record GetAllReportsQuery() : IRequest<List<ResultReportDTO>>;
}
