using GameWorld.Application.Mapping;
using GameWorld.Application.DTOs.GameWorldDTOs;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.GameWorld.Queries.GetListGameWorld
{
    public class GetListGameWorldHandler : IRequestHandler<GetGameWorldListQuery, IReadOnlyList<ResultGameWorldDTO>>
    {
        private readonly IReadRepository<Domain.Entities.GameWorld> _readRepo;
        private readonly GameWorldMapper _mapper;

        public GetListGameWorldHandler(IReadRepository<Domain.Entities.GameWorld> readRepo, GameWorldMapper mapper)
        {
            _readRepo = readRepo; _mapper = mapper;
        }

        public async ValueTask<IReadOnlyList<ResultGameWorldDTO>> Handle(GetGameWorldListQuery request, CancellationToken ct)
        {
            var list = await _readRepo.Table
                .Include(x => x.Seasons)
                .AsNoTracking()
                .ToListAsync(ct);

            return _mapper.ToResultDtoList(list).ToList();
        }
    }
}



