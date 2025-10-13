using MediatR;

namespace Moderation.Application.Features.Report.Commands.ResolveReport
{
    public record ResolveReportCommand(Guid ReportId, Guid ModeratorId) : IRequest<bool>;
}
