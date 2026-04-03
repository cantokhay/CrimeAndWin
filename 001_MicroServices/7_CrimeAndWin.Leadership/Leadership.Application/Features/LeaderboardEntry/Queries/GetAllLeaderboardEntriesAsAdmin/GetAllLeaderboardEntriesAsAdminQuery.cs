using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetAllLeaderboardEntriesAsAdmin
{
    public sealed record GetAllLeaderboardEntriesAsAdminQuery() : IRequest<List<AdminResultLeaderboardEntryDTO>>;
}


