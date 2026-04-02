using GameWorld.Application.Mapping;
using GameWorld.Application.DTOs.SeasonDTOs;
using Mediator;
using Shared.Domain.Repository;

namespace Season.Application.Features.Season.Queries
{
    public class GetByIdSeasonHandler : IRequestHandler<GetSeasonByIdQuery, ResultSeasonDTO>
    {
        private readonly IReadRepository<GameWorld.Domain.Entities.Season> _readRepo;
        private readonly GameWorldMapper _mapper;

        public GetByIdSeasonHandler(IReadRepository<GameWorld.Domain.Entities.Season> readRepo, GameWorldMapper mapper)
        {
            _readRepo = readRepo; _mapper = mapper;
        }

        public async ValueTask<ResultSeasonDTO> Handle(GetSeasonByIdQuery request, CancellationToken ct)
        {
            var s = await _readRepo.GetByIdAsync(request.Id.ToString());
            if (s is null) throw new KeyNotFoundException("Season not found.");
            return _mapper.ToResultDto(s);
        }
    }
}


