using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using MediatR;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetLeaderboardEntryByIdAsAdmin
{
    public sealed record GetLeaderboardEntryByIdAsAdminQuery(Guid id) : IRequest<AdminResultLeaderboardEntryDTO?>;
}
