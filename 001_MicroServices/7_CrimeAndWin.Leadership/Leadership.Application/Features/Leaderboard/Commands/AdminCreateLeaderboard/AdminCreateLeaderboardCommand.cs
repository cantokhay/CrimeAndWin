using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using MediatR;

namespace Leadership.Application.Features.Leaderboard.Commands.AdminCreateLeaderboard
{
    public sealed record AdminCreateLeaderboardCommand(AdminCreateLeaderboardDTO createLeaderboardDTO) : IRequest<Guid>;
}
