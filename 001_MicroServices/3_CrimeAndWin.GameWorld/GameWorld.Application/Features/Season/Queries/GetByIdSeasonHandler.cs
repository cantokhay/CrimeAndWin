using GameWorld.Application.Mapping;
using GameWorld.Application.DTOs.SeasonDTOs;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace Season.Application.Features.Season.Queries
{
    public class GetByIdSeasonHandler : IRequestHandler<GetSeasonByIdQuery, ResultSeasonDTO>
    {
        private readonly IReadRepository<GameWorld.Domain.Entities.Season> _readRepo;

        public GetByIdSeasonHandler(IReadRepository<GameWorld.Domain.Entities.Season> readRepo)
        {
            _readRepo = readRepo; 
        }

        public async Task<ResultSeasonDTO> Handle(GetSeasonByIdQuery request, CancellationToken ct)
        {
            var s = await _readRepo.GetByIdAsync(request.Id.ToString());
            if (s is null) throw new KeyNotFoundException("Season not found.");
            return GameWorldMapper.ToResultDto(s);
        }
    }
}



