using Leadership.Application.DTOs.LeaderboardDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Leadership.Application.Features.Leaderboard.Queries.GetLeaderbordById
{
    public record GetLeaderboardByIdQuery(Guid Id) : IRequest<ResultLeaderboardDTO>;
}


