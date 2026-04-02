using GameWorld.Application.Mapping;
using GameWorld.Application.DTOs.SeasonDTOs;
using Mediator;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.Season.Queries.GetAllSeason
{
    public sealed class GetAllSeasonsHandler(
            IReadRepository<Domain.Entities.Season> readRepo,
            GameWorldMapper mapper)
            : IRequestHandler<GetAllSeasonsQuery, List<ResultSeasonDTO>>
    {
        public async ValueTask<List<ResultSeasonDTO>> Handle(GetAllSeasonsQuery request, CancellationToken cancellationToken)
        {
            var query = readRepo.GetAll(tracking: false)
                .OrderByDescending(s => s.CreatedAtUtc);

            var list = query.ToList();
            return mapper.ToResultDtoList(list).ToList();
        }
    }
}



