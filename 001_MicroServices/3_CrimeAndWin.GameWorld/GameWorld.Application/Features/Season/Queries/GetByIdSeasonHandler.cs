using AutoMapper;
using GameWorld.Application.DTOs.SeasonDTOs;
using MediatR;
using Shared.Domain.Repository;

namespace Season.Application.Features.Season.Queries
{
    public class GetByIdSeasonHandler : IRequestHandler<GetSeasonByIdQuery, ResultSeasonDTO>
    {
        private readonly IReadRepository<GameWorld.Domain.Entities.Season> _readRepo;
        private readonly IMapper _mapper;

        public GetByIdSeasonHandler(IReadRepository<GameWorld.Domain.Entities.Season> readRepo, IMapper mapper)
        {
            _readRepo = readRepo; _mapper = mapper;
        }

        public async Task<ResultSeasonDTO> Handle(GetSeasonByIdQuery request, CancellationToken ct)
        {
            var s = await _readRepo.GetByIdAsync(request.Id.ToString());
            if (s is null) throw new KeyNotFoundException("Season not found.");
            return _mapper.Map<ResultSeasonDTO>(s);
        }
    }
}
