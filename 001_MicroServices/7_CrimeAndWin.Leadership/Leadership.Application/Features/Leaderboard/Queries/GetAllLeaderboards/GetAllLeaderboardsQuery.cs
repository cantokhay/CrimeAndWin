using Leadership.Application.DTOs.LeaderboardDTOs;
using Mediator;

namespace Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboards
{
    public sealed record GetAllLeaderboardsQuery() : IRequest<List<ResultLeaderboardDTO>>;
}


