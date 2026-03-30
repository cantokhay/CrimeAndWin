using MediatR;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;
using Shared.Domain.Repository;

namespace PlayerProfile.Application.Features.Player.Queries.GetByIdPlayerAsAdmin
{
    public sealed class GetPlayerByIdAsAdminQueryHandler : IRequestHandler<GetPlayerByIdAsAdminQuery, AdminResultPlayerDTO?>
    {
        private readonly IReadRepository<Domain.Entities.Player> _read;

        public GetPlayerByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.Player> read)
        {
            _read = read;
        }

        public async Task<AdminResultPlayerDTO?> Handle(GetPlayerByIdAsAdminQuery request, CancellationToken cancellationToken)
        {
            var entity = await _read.GetByIdAsync(request.Id.ToString(), tracking: false);
            if (entity is null) return null;

            return new AdminResultPlayerDTO
            {
                Id = entity.Id,
                AppUserId = entity.AppUserId,
                DisplayName = entity.DisplayName,
                AvatarKey = entity.AvatarKey,
                Power = entity.Stats.Power,
                Defense = entity.Stats.Defense,
                Agility = entity.Stats.Agility,
                Luck = entity.Stats.Luck,
                EnergyCurrent = entity.Energy.Current,
                EnergyMax = entity.Energy.Max,
                EnergyRegenPerMinute = entity.Energy.RegenPerMinute,
                RankPoints = entity.Rank.RankPoints,
                RankPosition = entity.Rank.Position,
                LastEnergyCalcUtc = entity.LastEnergyCalcUtc,
                CreatedAtUtc = entity.CreatedAtUtc,
                UpdatedAtUtc = entity.UpdatedAtUtc
            };
        }
    }
}
