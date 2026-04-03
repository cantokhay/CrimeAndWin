using Shared.Application.Abstractions.Messaging;

namespace Moderation.Application.Features.ModerationAction.Commands.AdminDeleteModerationAction
{
    public sealed record AdminDeleteModerationActionCommand(Guid id) : IRequest<bool>;
}


