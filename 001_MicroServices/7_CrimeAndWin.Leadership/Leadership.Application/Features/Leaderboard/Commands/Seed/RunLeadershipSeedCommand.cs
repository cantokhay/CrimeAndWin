using MediatR;

namespace Leadership.Application.Features.Leaderboard.Commands.Seed
{
    public sealed record RunLeadershipSeedCommand(int Count) : IRequest<Unit>;
}
