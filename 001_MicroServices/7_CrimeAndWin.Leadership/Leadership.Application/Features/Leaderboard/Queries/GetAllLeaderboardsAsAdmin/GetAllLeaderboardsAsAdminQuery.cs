using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboardsAsAdmin
{
    public sealed record GetAllLeaderboardsAsAdminQuery() : IRequest<List<AdminResultLeaderboardDTO>>;
}


