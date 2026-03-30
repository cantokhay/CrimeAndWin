using MediatR;
using PlayerProfile.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace PlayerProfile.Application.Features.Player.Commands.AdminCreatePlayer
{
    public sealed class AdminCreatePlayerCommandHandler : IRequestHandler<AdminCreatePlayerCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Player> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreatePlayerCommandHandler(IWriteRepository<Domain.Entities.Player> write, IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.createPlayerDTO;

            var entity = new Domain.Entities.Player
            {
                Id = Guid.NewGuid(),
                AppUserId = dto.AppUserId,
                DisplayName = dto.DisplayName,
                AvatarKey = dto.AvatarKey,
                Stats = new Stats(dto.Power, dto.Defense, dto.Agility, dto.Luck),
                Energy = new Energy(dto.EnergyCurrent, dto.EnergyMax, dto.EnergyRegenPerMinute),
                Rank = new Rank(dto.RankPoints, dto.RankPosition),
                LastEnergyCalcUtc = dto.LastEnergyCalcUtc == default ? _time.UtcNow : dto.LastEnergyCalcUtc,
                CreatedAtUtc = _time.UtcNow
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();

            return entity.Id;
        }
    }
}
