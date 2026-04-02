using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Mediator;

namespace Leadership.Application.Features.Leaderboard.Commands.AdminUpdateLeaderboard
{
    public sealed record AdminUpdateLeaderboardCommand(AdminUpdateLeaderboardDTO updateLeaderboardDTO) : IRequest<bool>;
}

