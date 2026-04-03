using PlayerProfile.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using PlayerProfile.Application.DTOs.PlayerDTOs;
using Shared.Domain.Repository;

namespace PlayerProfile.Application.Features.Player.Queries.GetAllPlayer
{
    public sealed class GetAllPlayersQueryHandler(
        IReadRepository<Domain.Entities.Player> readRepo,
        PlayerProfileMapper mapper)
        : IRequestHandler<GetAllPlayersQuery, List<ResultPlayerDTO>>
    {
        public async Task<List<ResultPlayerDTO>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            var query = readRepo.GetAll(tracking: false);

            var list = query
                .OrderByDescending(p => p.Rank.RankPoints)
                .ToList();

            return mapper.ToResultDtoList(list).ToList();
        }
    }
}



