using AutoMapper;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using MediatR;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetPlayerRank
{
    public class GetPlayerRankHandler : IRequestHandler<GetPlayerRankQuery, ResultLeaderboardEntryDTO>
    {
        private readonly IReadRepository<Domain.Entities.LeaderboardEntry> _readRepo;
        private readonly IMapper _mapper;


        public GetPlayerRankHandler(IReadRepository<Domain.Entities.LeaderboardEntry> readRepo, IMapper mapper)
        { _readRepo = readRepo; _mapper = mapper; }


        public async Task<ResultLeaderboardEntryDTO> Handle(GetPlayerRankQuery request, CancellationToken cancellationToken)
        {
            var entry = await _readRepo.GetSingleAsync(x => x.LeaderboardId == request.LeaderboardId && x.PlayerId == request.PlayerId);
            return entry is null ? null : _mapper.Map<ResultLeaderboardEntryDTO>(entry);
        }
    }
}
