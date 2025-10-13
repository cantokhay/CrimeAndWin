using Leadership.Application.DTOs.LeaderboardDTOs;
using MediatR;

namespace Leadership.Application.Features.Leaderboard.Commands.CreateLeaderboard
{
    public record CreateLeaderboardCommand(CreateLeaderboardDTO Dto) : IRequest<Guid>;
}
