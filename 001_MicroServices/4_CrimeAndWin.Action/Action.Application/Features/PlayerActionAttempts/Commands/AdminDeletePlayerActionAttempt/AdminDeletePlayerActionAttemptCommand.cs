using MediatR;

namespace Action.Application.Features.PlayerActionAttempts.Commands.AdminDeletePlayerActionAttempt
{
    public sealed record AdminDeletePlayerActionAttemptCommand(Guid Id) : IRequest<bool>;
}

