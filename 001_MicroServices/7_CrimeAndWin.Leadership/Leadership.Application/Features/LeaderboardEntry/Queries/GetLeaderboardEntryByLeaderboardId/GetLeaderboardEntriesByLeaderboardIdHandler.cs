using Leadership.Application.Mapping;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetLeaderboardEntryByLeaderboardId
{
    public class GetEntriesByLeaderboardIdHandler : IRequestHandler<GetEntriesByLeaderboardIdQuery, List<ResultLeaderboardEntryDTO>>
    {
        private readonly IReadRepository<Domain.Entities.LeaderboardEntry> _readRepo;
        private readonly LeadershipMapper _mapper;

        public GetEntriesByLeaderboardIdHandler(IReadRepository<Domain.Entities.LeaderboardEntry> readRepo, LeadershipMapper mapper)
        { _readRepo = readRepo; _mapper = mapper; }


        public async ValueTask<List<ResultLeaderboardEntryDTO>> Handle(GetEntriesByLeaderboardIdQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.Page - 1) * request.PageSize;
            var list = await _readRepo.GetWhere(x => x.LeaderboardId == request.LeaderboardId && x.IsActive)
            .OrderByDescending(x => x.Rank.RankPoints)
            .Skip(skip)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);


            return _mapper.ToDtoList(list).ToList();
        }
    }
}


