using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using MediatR;

namespace Leadership.Application.Features.Leaderboard.Commands.AdminUpdateLeaderboard
{
    public sealed record AdminUpdateLeaderboardCommand(AdminUpdateLeaderboardDTO updateLeaderboardDTO) : IRequest<bool>;
}
