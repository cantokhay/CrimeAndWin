using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetLeaderboardEntryByIdAsAdmin
{
    public sealed record GetLeaderboardEntryByIdAsAdminQuery(Guid id) : IRequest<AdminResultLeaderboardEntryDTO?>;
}


