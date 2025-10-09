using AutoMapper;
using MediatR;
using PlayerProfile.Application.DTOs.PlayerDTOs;
using Shared.Domain.Repository;

namespace PlayerProfile.Application.Features.Player.Queries.GetAllPlayer
{
    public sealed class GetAllPlayersHandler(
        IReadRepository<Domain.Entities.Player> readRepo,
        IMapper mapper)
        : IRequestHandler<GetAllPlayersQuery, List<ResultPlayerDTO>>
    {
        public async Task<List<ResultPlayerDTO>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            var query = readRepo.GetAll(tracking: false);

            var list = query
                .OrderByDescending(p => p.Rank.RankPoints)
                .ToList();

            return mapper.Map<List<ResultPlayerDTO>>(list);
        }
    }
}
