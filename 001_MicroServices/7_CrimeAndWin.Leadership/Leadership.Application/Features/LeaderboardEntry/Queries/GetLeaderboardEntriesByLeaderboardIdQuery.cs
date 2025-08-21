using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using MediatR;

namespace Leadership.Application.Features.LeaderboardEntry.Queries
{
    public record GetEntriesByLeaderboardIdQuery(Guid LeaderboardId, int Page = 1, int PageSize = 20) : IRequest<List<ResultLeaderboardEntryDTO>>;
}
