using Action.Application.DTOs.ActionAttemptDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.PlayerActionAttempts.Commands.PerformPlayerAction
{
    public sealed record PerformPlayerActionCommand(PlayerActionAttemptDTO Request)
        : IRequest<PerformPlayerActionResult>;
}


