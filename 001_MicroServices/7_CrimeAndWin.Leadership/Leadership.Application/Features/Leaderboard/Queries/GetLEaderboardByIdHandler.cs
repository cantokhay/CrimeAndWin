using AutoMapper;
using Leadership.Application.DTOs.LeaderboardDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.Leaderboard.Queries
{
    public class GetLeaderboardByIdHandler : IRequestHandler<GetLeaderboardByIdQuery, ResultLeaderboardDTO>
    {
        private readonly IReadRepository<Domain.Entities.Leaderboard> _readRepo;
        private readonly IMapper _mapper;


        public GetLeaderboardByIdHandler(IReadRepository<Domain.Entities.Leaderboard> readRepo, IMapper mapper)
        { _readRepo = readRepo; _mapper = mapper; }


        public async Task<ResultLeaderboardDTO> Handle(GetLeaderboardByIdQuery request, CancellationToken cancellationToken)
        {
            var query = _readRepo.GetWhere(x => x.Id == request.Id)
            .Include(x => x.Entries);
            var entity = await query.FirstOrDefaultAsync(cancellationToken);
            return entity is null ? null : _mapper.Map<ResultLeaderboardDTO>(entity);
        }
    }
}
