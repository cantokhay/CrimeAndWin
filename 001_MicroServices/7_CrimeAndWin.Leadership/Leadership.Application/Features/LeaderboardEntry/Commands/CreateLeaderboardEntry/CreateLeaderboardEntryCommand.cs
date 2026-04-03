using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.CreateLeaderboardEntry
{
    public record CreateLeaderboardEntryCommand(Guid LeaderboardId, CreateLeaderboardEntryDTO Dto) : IRequest<Guid>;
}


