using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using MediatR;

namespace Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboardsAsAdmin
{
    public sealed record GetAllLeaderboardsAsAdminQuery() : IRequest<List<AdminResultLeaderboardDTO>>;
}
