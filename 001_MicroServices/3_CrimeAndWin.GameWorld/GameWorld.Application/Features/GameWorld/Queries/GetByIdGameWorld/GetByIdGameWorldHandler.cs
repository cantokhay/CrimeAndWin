using AutoMapper;
using GameWorld.Application.DTOs.GameWorldDTOs;
using MediatR;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.GameWorld.Queries.GetByIdGameWorld
{
    public class GetByIdGameWorldHandler : IRequestHandler<GetGameWorldByIdQuery, ResultGameWorldDTO>
    {   
        private readonly IReadRepository<Domain.Entities.GameWorld> _readRepo;
        private readonly IMapper _mapper;

        public GetByIdGameWorldHandler(IReadRepository<Domain.Entities.GameWorld> readRepo, IMapper mapper)
        {
            _readRepo = readRepo; _mapper = mapper;
        }

        public async Task<ResultGameWorldDTO> Handle(GetGameWorldByIdQuery request, CancellationToken ct)
        {
            var gw = await _readRepo.GetByIdAsync(request.Id.ToString());
            if (gw is null) throw new KeyNotFoundException("GameWorld not found.");
            return _mapper.Map<ResultGameWorldDTO>(gw);
        }
    }
}
