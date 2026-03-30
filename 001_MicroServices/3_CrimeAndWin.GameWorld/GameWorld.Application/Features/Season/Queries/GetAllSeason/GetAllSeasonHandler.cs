using AutoMapper;
using GameWorld.Application.DTOs.SeasonDTOs;
using MediatR;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.Season.Queries.GetAllSeason
{
    public sealed class GetAllSeasonsHandler(
            IReadRepository<Domain.Entities.Season> readRepo,
            IMapper mapper)
            : IRequestHandler<GetAllSeasonsQuery, List<ResultSeasonDTO>>
    {
        public async Task<List<ResultSeasonDTO>> Handle(GetAllSeasonsQuery request, CancellationToken cancellationToken)
        {
            var query = readRepo.GetAll(tracking: false)
                .OrderByDescending(s => s.CreatedAtUtc);

            var list = query.ToList();
            return mapper.Map<List<ResultSeasonDTO>>(list);
        }
    }
}
