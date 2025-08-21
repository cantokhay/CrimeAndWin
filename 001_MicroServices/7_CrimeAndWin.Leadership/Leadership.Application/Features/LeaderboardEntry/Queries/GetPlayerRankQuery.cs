using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using MediatR;

namespace Leadership.Application.Features.LeaderboardEntry.Queries
{
    public record GetPlayerRankQuery(Guid LeaderboardId, Guid PlayerId) : IRequest<ResultLeaderboardEntryDTO>;
}
