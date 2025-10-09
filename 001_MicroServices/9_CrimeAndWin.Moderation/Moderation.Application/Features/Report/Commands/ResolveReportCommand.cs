using MediatR;

namespace Moderation.Application.Features.Report.Commands
{
    public record ResolveReportCommand(Guid ReportId, Guid ModeratorId) : IRequest<bool>;
}
