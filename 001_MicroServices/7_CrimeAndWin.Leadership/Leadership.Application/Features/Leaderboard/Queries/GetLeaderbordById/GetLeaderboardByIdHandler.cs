using Leadership.Application.Mapping;
using Leadership.Application.DTOs.LeaderboardDTOs;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.Leaderboard.Queries.GetLeaderbordById
{
    public class GetLeaderboardByIdHandler : IRequestHandler<GetLeaderboardByIdQuery, ResultLeaderboardDTO>
    {
        private readonly IReadRepository<Domain.Entities.Leaderboard> _readRepo;
        private readonly LeadershipMapper _mapper;

        public GetLeaderboardByIdHandler(IReadRepository<Domain.Entities.Leaderboard> readRepo, LeadershipMapper mapper)
        { _readRepo = readRepo; _mapper = mapper; }


        public async ValueTask<ResultLeaderboardDTO> Handle(GetLeaderboardByIdQuery request, CancellationToken cancellationToken)
        {
            var query = _readRepo.GetWhere(x => x.Id == request.Id)
            .Include(x => x.Entries);
            var entity = await query.FirstOrDefaultAsync(cancellationToken);
            return entity is null ? null : _mapper.ToDto(entity);
        }
    }
}

