using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetAllLeaderboardEntries
{
    public sealed record GetAllLeaderboardEntriesQuery() : IRequest<List<ResultLeaderboardEntryDTO>>;
}


