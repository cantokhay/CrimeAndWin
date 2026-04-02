using GameWorld.Application.Mapping;
using GameWorld.Application.DTOs.GameWorldDTOs;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.GameWorld.Queries.GetByIdGameWorld
{
    public class GetByIdGameWorldHandler : IRequestHandler<GetGameWorldByIdQuery, ResultGameWorldDTO>
    {   
        private readonly IReadRepository<Domain.Entities.GameWorld> _readRepo;
        private readonly GameWorldMapper _mapper;

        public GetByIdGameWorldHandler(IReadRepository<Domain.Entities.GameWorld> readRepo, GameWorldMapper mapper)
        {
            _readRepo = readRepo; _mapper = mapper;
        }

        public async ValueTask<ResultGameWorldDTO> Handle(GetGameWorldByIdQuery request, CancellationToken ct)
        {
            var gameWorld = await _readRepo.Table
                        .Include(x => x.Seasons)
                        .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (gameWorld is null) throw new KeyNotFoundException("GameWorld not found.");
            return _mapper.ToResultDto(gameWorld);
        }
    }
}


