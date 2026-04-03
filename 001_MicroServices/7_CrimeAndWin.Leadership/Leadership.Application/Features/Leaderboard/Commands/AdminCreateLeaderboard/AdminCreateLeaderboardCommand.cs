using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.Leaderboard.Commands.AdminCreateLeaderboard
{
    public sealed record AdminCreateLeaderboardCommand(AdminCreateLeaderboardDTO createLeaderboardDTO) : IRequest<Guid>;
}


