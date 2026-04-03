using Leadership.Application.DTOs.LeaderboardDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.Leaderboard.Commands.CreateLeaderboard
{
    public record CreateLeaderboardCommand(CreateLeaderboardDTO Dto) : IRequest<Guid>;
}


