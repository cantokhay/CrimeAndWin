using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using MediatR;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetAllLeaderboardEntries
{
    public sealed record GetAllLeaderboardEntriesQuery() : IRequest<List<ResultLeaderboardEntryDTO>>;
}
