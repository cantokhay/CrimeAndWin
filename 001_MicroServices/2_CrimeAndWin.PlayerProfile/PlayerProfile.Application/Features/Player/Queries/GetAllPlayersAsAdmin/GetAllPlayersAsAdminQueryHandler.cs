using MediatR;
using Microsoft.EntityFrameworkCore;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;
using Shared.Domain.Repository;

namespace PlayerProfile.Application.Features.Player.Queries.GetAllPlayersAsAdmin
{
    public sealed class GetAllPlayersAsAdminQueryHandler : IRequestHandler<GetAllPlayersAsAdminQuery, List<AdminResultPlayerDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Player> _read;

        public GetAllPlayersAsAdminQueryHandler(IReadRepository<Domain.Entities.Player> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultPlayerDTO>> Handle(GetAllPlayersAsAdminQuery request, CancellationToken cancellationToken)
        {
            return await _read.GetAll(false)
                .Select(p => new AdminResultPlayerDTO
                {
                    Id = p.Id,
                    AppUserId = p.AppUserId,
                    DisplayName = p.DisplayName,
                    AvatarKey = p.AvatarKey,
                    Power = p.Stats.Power,
                    Defense = p.Stats.Defense,
                    Agility = p.Stats.Agility,
                    Luck = p.Stats.Luck,
                    EnergyCurrent = p.Energy.Current,
                    EnergyMax = p.Energy.Max,
                    EnergyRegenPerMinute = p.Energy.RegenPerMinute,
                    RankPoints = p.Rank.RankPoints,
                    RankPosition = p.Rank.Position,
                    LastEnergyCalcUtc = p.LastEnergyCalcUtc,
                    CreatedAtUtc = p.CreatedAtUtc,
                    UpdatedAtUtc = p.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
