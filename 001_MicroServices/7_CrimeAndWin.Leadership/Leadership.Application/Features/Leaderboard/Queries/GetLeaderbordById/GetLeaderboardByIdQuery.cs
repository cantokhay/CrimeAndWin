using Leadership.Application.DTOs.LeaderboardDTOs;
using Mediator;

namespace Leadership.Application.Features.Leaderboard.Queries.GetLeaderbordById
{
    public record GetLeaderboardByIdQuery(Guid Id) : IRequest<ResultLeaderboardDTO>;
}

