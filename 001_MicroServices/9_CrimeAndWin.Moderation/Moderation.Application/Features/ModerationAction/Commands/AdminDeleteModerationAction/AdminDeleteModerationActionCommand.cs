using MediatR;

namespace Moderation.Application.Features.ModerationAction.Commands.AdminDeleteModerationAction
{
    public sealed record AdminDeleteModerationActionCommand(Guid id) : IRequest<bool>;
}
