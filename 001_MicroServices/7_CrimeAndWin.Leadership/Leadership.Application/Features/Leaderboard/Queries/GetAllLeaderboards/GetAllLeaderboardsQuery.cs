using Leadership.Application.DTOs.LeaderboardDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboards
{
    public sealed record GetAllLeaderboardsQuery() : IRequest<List<ResultLeaderboardDTO>>;
}



