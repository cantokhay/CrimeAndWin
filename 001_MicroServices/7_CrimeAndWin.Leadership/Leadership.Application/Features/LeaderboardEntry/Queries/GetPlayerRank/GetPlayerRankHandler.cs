using Leadership.Application.Mapping;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Mediator;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetPlayerRank
{
    public class GetPlayerRankHandler : IRequestHandler<GetPlayerRankQuery, ResultLeaderboardEntryDTO>
    {
        private readonly IReadRepository<Domain.Entities.LeaderboardEntry> _readRepo;
        private readonly LeadershipMapper _mapper;

        public GetPlayerRankHandler(IReadRepository<Domain.Entities.LeaderboardEntry> readRepo, LeadershipMapper mapper)
        { _readRepo = readRepo; _mapper = mapper; }


        public async ValueTask<ResultLeaderboardEntryDTO> Handle(GetPlayerRankQuery request, CancellationToken cancellationToken)
        {
            var entry = await _readRepo.GetSingleAsync(x => x.LeaderboardId == request.LeaderboardId && x.PlayerId == request.PlayerId);
            return entry is null ? null : _mapper.ToResultDto(entry);
        }
    }
}

