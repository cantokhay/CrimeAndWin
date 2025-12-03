using MediatR;

namespace Leadership.Application.Features.Leaderboard.Commands.AdminDeleteLeaderboard
{
    public sealed record AdminDeleteLeaderboardCommand(Guid id) : IRequest<bool>;
}
