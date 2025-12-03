using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using MediatR;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.AdminCreateLeaderboardEntry
{
    public sealed record AdminCreateLeaderboardEntryCommand(AdminCreateLeaderboardEntryDTO createLeaderboardEntryDTO) : IRequest<Guid>;
}
