using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using MediatR;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.CreateLeaderboardEntry
{
    public record CreateLeaderboardEntryCommand(Guid LeaderboardId, CreateLeaderboardEntryDTO Dto) : IRequest<Guid>;
}
