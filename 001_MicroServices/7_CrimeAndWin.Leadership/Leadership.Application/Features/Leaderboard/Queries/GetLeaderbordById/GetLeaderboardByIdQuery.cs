using Leadership.Application.DTOs.LeaderboardDTOs;
using MediatR;

namespace Leadership.Application.Features.Leaderboard.Queries.GetLeaderbordById
{
    public record GetLeaderboardByIdQuery(Guid Id) : IRequest<ResultLeaderboardDTO>;
}
