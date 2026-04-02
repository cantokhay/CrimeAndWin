using Action.Application.DTOs.ActionAttemptDTOs;
using Mediator;

namespace Action.Application.Features.PlayerActionAttempts.Commands.PerformPlayerAction
{
    public sealed record PerformPlayerActionCommand(PlayerActionAttemptDTO Request)
        : IRequest<Guid>; // returns AttemptId
}

