using Mediator;

namespace Moderation.Application.Features.Report.Commands.AdminDeleteReport
{
    public sealed record AdminDeleteReportCommand(Guid id) : IRequest<bool>;
}

