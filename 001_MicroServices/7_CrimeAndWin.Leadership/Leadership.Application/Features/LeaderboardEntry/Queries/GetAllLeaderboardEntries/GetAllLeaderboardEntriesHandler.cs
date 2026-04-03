using Leadership.Application.Mapping;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Shared.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetAllLeaderboardEntries
{
    public sealed class GetAllLeaderboardEntriesHandler(
            IReadRepository<Domain.Entities.LeaderboardEntry> readRepo,
            LeadershipMapper mapper)
            : IRequestHandler<GetAllLeaderboardEntriesQuery, List<ResultLeaderboardEntryDTO>>
    {
        public async Task<List<ResultLeaderboardEntryDTO>> Handle(GetAllLeaderboardEntriesQuery request, CancellationToken cancellationToken)
        {
            var entries = await readRepo.Table
                .AsNoTracking()
                .OrderByDescending(e => e.Rank.RankPoints)
                .ToListAsync(cancellationToken);

            return mapper.ToResultDtoList(entries).ToList();
        }
    }
}



