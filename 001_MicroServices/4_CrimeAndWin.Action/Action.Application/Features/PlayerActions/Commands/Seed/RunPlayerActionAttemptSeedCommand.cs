using MediatR;

namespace Action.Application.Features.PlayerActions.Commands.Seed
{
    public sealed record RunPlayerActionAttemptSeedCommand(int Count) : IRequest<Unit>;
}
