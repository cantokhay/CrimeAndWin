using Leadership.Application.Mapping;
using Leadership.Application.DTOs.LeaderboardDTOs;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboards
{
    public sealed class GetAllLeaderboardsHandler(
        IReadRepository<Domain.Entities.Leaderboard> readRepo,
        LeadershipMapper mapper)
        : IRequestHandler<GetAllLeaderboardsQuery, List<ResultLeaderboardDTO>>
    {
        public async ValueTask<List<ResultLeaderboardDTO>> Handle(GetAllLeaderboardsQuery request, CancellationToken cancellationToken)
        {
            var leaderboards = await readRepo.Table
                .Include(x => x.Entries)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.ToResultDtoList(leaderboards).ToList();
        }
    }
}


