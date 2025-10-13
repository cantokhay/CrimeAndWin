using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using MediatR;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetLeaderboardEntryByLeaderboardId
{
    public record GetEntriesByLeaderboardIdQuery(Guid LeaderboardId, int Page = 1, int PageSize = 20) : IRequest<List<ResultLeaderboardEntryDTO>>;
}
