using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.AdminCreateLeaderboardEntry
{
    public sealed record AdminCreateLeaderboardEntryCommand(AdminCreateLeaderboardEntryDTO createLeaderboardEntryDTO) : IRequest<Guid>;
}


