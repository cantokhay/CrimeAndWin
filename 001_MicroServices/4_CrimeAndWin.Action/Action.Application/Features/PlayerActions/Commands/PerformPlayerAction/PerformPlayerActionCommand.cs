using Action.Application.DTOs;
using MediatR;

namespace Action.Application.Features.PlayerActions.Commands.PerformPlayerAction
{
    public sealed record PerformPlayerActionCommand(PlayerActionAttemptDTO Request)
        : IRequest<Guid>; // returns AttemptId
}
