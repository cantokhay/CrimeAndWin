using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.Leaderboard.Queries.GetLeaderboardByIdAsAdmin
{
    public sealed record GetLeaderboardByIdAsAdminQuery(Guid id) : IRequest<AdminResultLeaderboardDTO?>;
}


