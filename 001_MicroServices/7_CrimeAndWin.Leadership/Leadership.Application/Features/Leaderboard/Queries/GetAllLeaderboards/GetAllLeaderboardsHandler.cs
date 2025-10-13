using AutoMapper;
using Leadership.Application.DTOs.LeaderboardDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboards
{
    public sealed class GetAllLeaderboardsHandler(
        IReadRepository<Domain.Entities.Leaderboard> readRepo,
        IMapper mapper)
        : IRequestHandler<GetAllLeaderboardsQuery, List<ResultLeaderboardDTO>>
    {
        public async Task<List<ResultLeaderboardDTO>> Handle(GetAllLeaderboardsQuery request, CancellationToken cancellationToken)
        {
            var leaderboards = await readRepo.Table
                .Include(x => x.Entries)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.Map<List<ResultLeaderboardDTO>>(leaderboards);
        }
    }
}
