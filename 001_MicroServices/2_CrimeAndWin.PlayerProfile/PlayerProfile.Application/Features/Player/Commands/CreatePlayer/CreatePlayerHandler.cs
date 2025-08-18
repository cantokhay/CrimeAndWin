using AutoMapper;
using MediatR;
using PlayerProfile.Application.Features.Player.DTOs;
using PlayerProfile.Domain.VOs;
using Shared.Domain.Repository;

namespace PlayerProfile.Application.Features.Player.Commands.CreatePlayer
{
    public sealed class CreatePlayerHandler(
        IWriteRepository<Domain.Entities.Player> repo, IMapper mapper) : IRequestHandler<CreatePlayerCommand, CreatePlayerDTO>
    {
        public async Task<CreatePlayerDTO> Handle(CreatePlayerCommand r, CancellationToken ct)
        {
            var entity = new Domain.Entities.Player
            {
                AppUserId = r.AppUserId,
                DisplayName = r.DisplayName,
                AvatarKey = r.AvatarKey,
                Stats = new Stats(r.Power, r.Defense, r.Agility, r.Luck),
                Energy = new Energy(r.EnergyCurrent, r.EnergyMax, r.EnergyRegenPerMinute),
                Rank = new Rank(0, null),
                LastEnergyCalcUtc = DateTime.UtcNow
            };
            await repo.AddAsync(entity);
            await repo.SaveAsync();
            return mapper.Map<CreatePlayerDTO>(entity);
        }
    }
}
