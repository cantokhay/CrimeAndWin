using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using Mediator;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetAllLeaderboardEntriesAsAdmin
{
    public sealed record GetAllLeaderboardEntriesAsAdminQuery() : IRequest<List<AdminResultLeaderboardEntryDTO>>;
}

