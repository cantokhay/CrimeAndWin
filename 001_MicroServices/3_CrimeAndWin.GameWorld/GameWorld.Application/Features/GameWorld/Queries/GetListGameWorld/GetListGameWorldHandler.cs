using AutoMapper;
using GameWorld.Application.DTOs.GameWorldDTOs;
using MediatR;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.GameWorld.Queries.GetListGameWorld
{
    public class GetListGameWorldHandler : IRequestHandler<GetGameWorldListQuery, IReadOnlyList<ResultGameWorldDTO>>
    {
        private readonly IReadRepository<Domain.Entities.GameWorld> _readRepo;
        private readonly IMapper _mapper;

        public GetListGameWorldHandler(IReadRepository<Domain.Entities.GameWorld> readRepo, IMapper mapper)
        {
            _readRepo = readRepo; _mapper = mapper;
        }

        public async Task<IReadOnlyList<ResultGameWorldDTO>> Handle(GetGameWorldListQuery request, CancellationToken ct)
        {
            var list = _readRepo.GetAll();
            return list.Select(_mapper.Map<ResultGameWorldDTO>).ToList();
        }
    }
}
