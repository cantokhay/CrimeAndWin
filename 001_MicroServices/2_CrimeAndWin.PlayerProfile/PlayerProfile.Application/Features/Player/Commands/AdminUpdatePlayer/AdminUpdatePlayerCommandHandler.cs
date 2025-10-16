using MediatR;
using PlayerProfile.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace PlayerProfile.Application.Features.Player.Commands.AdminUpdatePlayer
{
    public sealed class AdminUpdatePlayerCommandHandler : IRequestHandler<AdminUpdatePlayerCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Player> _write;
        private readonly IReadRepository<Domain.Entities.Player> _read;
        private readonly IDateTimeProvider _time;

        public AdminUpdatePlayerCommandHandler(IWriteRepository<Domain.Entities.Player> write, IReadRepository<Domain.Entities.Player> read, IDateTimeProvider time)
        {
            _write = write;
            _read = read;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.updatePlayerDTO;
            var entity = await _read.GetByIdAsync(dto.Id.ToString(), tracking: true);
            if (entity is null)
                return false;

            entity.AppUserId = dto.AppUserId;
            entity.DisplayName = dto.DisplayName;
            entity.AvatarKey = dto.AvatarKey;
            entity.Stats = new Stats(dto.Power, dto.Defense, dto.Agility, dto.Luck);
            entity.Energy = new Energy(dto.EnergyCurrent, dto.EnergyMax, dto.EnergyRegenPerMinute);
            entity.Rank = new Rank(dto.RankPoints, dto.RankPosition);
            entity.LastEnergyCalcUtc = dto.LastEnergyCalcUtc;
            entity.UpdatedAtUtc = _time.UtcNow;

            var result = _write.Update(entity);
            await _write.SaveAsync();
            return result;
        }
    }
}
