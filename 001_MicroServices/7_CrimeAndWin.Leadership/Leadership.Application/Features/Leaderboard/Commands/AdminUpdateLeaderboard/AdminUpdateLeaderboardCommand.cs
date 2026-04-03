using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.Leaderboard.Commands.AdminUpdateLeaderboard
{
    public sealed record AdminUpdateLeaderboardCommand(AdminUpdateLeaderboardDTO updateLeaderboardDTO) : IRequest<bool>;
}


