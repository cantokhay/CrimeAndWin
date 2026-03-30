using MediatR;

namespace Action.Application.Features.PlayerActionAttempts.Commands.Seed
{
    public sealed record RunPlayerActionAttemptSeedCommand(int Count) : IRequest<Unit>;
}
