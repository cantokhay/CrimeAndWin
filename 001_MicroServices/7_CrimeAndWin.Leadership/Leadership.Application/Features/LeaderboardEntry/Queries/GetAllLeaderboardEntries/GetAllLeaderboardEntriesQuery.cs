using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Mediator;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetAllLeaderboardEntries
{
    public sealed record GetAllLeaderboardEntriesQuery() : IRequest<List<ResultLeaderboardEntryDTO>>;
}

