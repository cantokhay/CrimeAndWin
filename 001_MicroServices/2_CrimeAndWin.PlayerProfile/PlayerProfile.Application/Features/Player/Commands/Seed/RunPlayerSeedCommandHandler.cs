using Shared.Application.Abstractions.Messaging;
using PlayerProfile.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace PlayerProfile.Application.Features.Player.Commands.Seed
{
    public sealed class RunPlayerSeedCommandHandler : IRequestHandler<RunPlayerSeedCommand, Unit>
    {
        private readonly IWriteRepository<global::PlayerProfile.Domain.Entities.Player> _writeRepository;
        private readonly IReadRepository<global::PlayerProfile.Domain.Entities.Player> _readRepository;

        public RunPlayerSeedCommandHandler(
            IWriteRepository<global::PlayerProfile.Domain.Entities.Player> writeRepository, 
            IReadRepository<global::PlayerProfile.Domain.Entities.Player> readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<Unit> Handle(RunPlayerSeedCommand request, CancellationToken cancellationToken)
        {
            var seedDate = SeedDataConstants.SeedDate;

            // Core Players
            var corePlayers = new List<global::PlayerProfile.Domain.Entities.Player>
            {
                new()
                {
                    Id = SeedDataConstants.PlayerAlphaId,
                    AppUserId = SeedDataConstants.UserAlphaId,
                    DisplayName = "Alpha",
                    AvatarKey = "avatar_alpha",
                    Stats = new Stats(Power: 100, Defense: 80, Agility: 90, Luck: 50),
                    Energy = new Energy(Current: 100, Max: 100, RegenPerMinute: 5),
                    Rank = new Rank(RankPoints: 1000, Position: 1),
                    CreatedAtUtc = seedDate,
                    LastEnergyCalcUtc = seedDate
                },
                new()
                {
                    Id = SeedDataConstants.PlayerBetaId,
                    AppUserId = SeedDataConstants.UserBetaId,
                    DisplayName = "Beta",
                    AvatarKey = "avatar_beta",
                    Stats = new Stats(Power: 70, Defense: 60, Agility: 65, Luck: 30),
                    Energy = new Energy(Current: 100, Max: 100, RegenPerMinute: 5),
                    Rank = new Rank(RankPoints: 500, Position: 2),
                    CreatedAtUtc = seedDate,
                    LastEnergyCalcUtc = seedDate
                }
            };

            foreach (var p in corePlayers)
            {
                if (await _readRepository.GetByIdAsync(p.Id.ToString()) == null)
                {
                    await _writeRepository.AddAsync(p);
                }
            }
            await _writeRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
