using Action.Application.DTOs.ActionAttemptDTOs;
using MediatR;

namespace Action.Application.Features.PlayerActionAttempts.Commands.PerformPlayerAction
{
    public sealed record PerformPlayerActionCommand(PlayerActionAttemptDTO Request)
        : IRequest<Guid>; // returns AttemptId
}
