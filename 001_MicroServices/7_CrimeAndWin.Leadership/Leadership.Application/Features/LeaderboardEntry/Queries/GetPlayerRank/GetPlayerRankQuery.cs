using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetPlayerRank
{
    public record GetPlayerRankQuery(Guid LeaderboardId, Guid PlayerId) : IRequest<ResultLeaderboardEntryDTO>;
}


