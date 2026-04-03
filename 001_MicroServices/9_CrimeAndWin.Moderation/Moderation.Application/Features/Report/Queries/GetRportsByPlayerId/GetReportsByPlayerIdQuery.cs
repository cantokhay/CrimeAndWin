using Shared.Application.Abstractions.Messaging;
using Moderation.Application.DTOs.ReportDTOs;

namespace Moderation.Application.Features.Report.Queries.GetRportsByPlayerId
{
    public record GetReportsByPlayerIdQuery(Guid ReportedPlayerId) : IRequest<List<ResultReportDTO>>;
}


