using AutoMapper;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Queries
{
    public class GetEntriesByLeaderboardIdHandler : IRequestHandler<GetEntriesByLeaderboardIdQuery, List<ResultLeaderboardEntryDTO>>
    {
        private readonly IReadRepository<Domain.Entities.LeaderboardEntry> _readRepo;
        private readonly IMapper _mapper;


        public GetEntriesByLeaderboardIdHandler(IReadRepository<Domain.Entities.LeaderboardEntry> readRepo, IMapper mapper)
        { _readRepo = readRepo; _mapper = mapper; }


        public async Task<List<ResultLeaderboardEntryDTO>> Handle(GetEntriesByLeaderboardIdQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.Page - 1) * request.PageSize;
            var list = await _readRepo.GetWhere(x => x.LeaderboardId == request.LeaderboardId && x.IsActive)
            .OrderByDescending(x => x.Rank.RankPoints)
            .Skip(skip)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);


            return list.Select(x => _mapper.Map<ResultLeaderboardEntryDTO>(x)).ToList();
        }
    }
}
