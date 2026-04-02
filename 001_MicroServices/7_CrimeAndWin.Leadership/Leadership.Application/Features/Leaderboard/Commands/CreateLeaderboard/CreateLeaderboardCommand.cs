using Leadership.Application.DTOs.LeaderboardDTOs;
using Mediator;

namespace Leadership.Application.Features.Leaderboard.Commands.CreateLeaderboard
{
    public record CreateLeaderboardCommand(CreateLeaderboardDTO Dto) : IRequest<Guid>;
}

