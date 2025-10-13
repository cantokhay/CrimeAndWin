using AutoMapper;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetAllLeaderboardEntries
{
    public sealed class GetAllLeaderboardEntriesHandler(
            IReadRepository<Domain.Entities.LeaderboardEntry> readRepo,
            IMapper mapper)
            : IRequestHandler<GetAllLeaderboardEntriesQuery, List<ResultLeaderboardEntryDTO>>
    {
        public async Task<List<ResultLeaderboardEntryDTO>> Handle(GetAllLeaderboardEntriesQuery request, CancellationToken cancellationToken)
        {
            var entries = await readRepo.Table
                .AsNoTracking()
                .OrderByDescending(e => e.Rank.RankPoints)
                .ToListAsync(cancellationToken);

            return mapper.Map<List<ResultLeaderboardEntryDTO>>(entries);
        }
    }
}
