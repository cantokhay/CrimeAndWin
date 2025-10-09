using MediatR;
using Moderation.Application.DTOs.ReportDTOs;

namespace Moderation.Application.Features.Report.Queries
{
    public record GetReportsByPlayerIdQuery(Guid ReportedPlayerId) : IRequest<List<ResultReportDTO>>;
}
