using Bogus;
using Shared.Application.Abstractions.Messaging;
using PlayerProfile.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace PlayerProfile.Application.Features.Player.Commands.Seed
{
    public sealed class RunPlayerSeedCommandHandler : IRequestHandler<RunPlayerSeedCommand, Unit>
    {
        private readonly IWriteRepository<Domain.Entities.Player> _writeRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public RunPlayerSeedCommandHandler(IWriteRepository<Domain.Entities.Player> writeRepository, IDateTimeProvider dateTimeProvider)
        {
            _writeRepository = writeRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Unit> Handle(RunPlayerSeedCommand request, CancellationToken cancellationToken)
        {
            var now = _dateTimeProvider.UtcNow;
            var themeUsers = new[] { "Boss", "Hitman", "Mole", "Fixer", "Dealer", "Enforcer", "Launderer" };
            var players = new List<Domain.Entities.Player>();

            for (int i = 0; i < themeUsers.Length; i++)
            {
                var name = themeUsers[i];
                var player = new Domain.Entities.Player
                {
                    Id = Guid.Parse($"22222222-2222-2222-2222-{i:D12}"),
                    AppUserId = Guid.Parse($"00000000-0000-0000-0000-{i:D12}"), // Maps to Identity
                    DisplayName = name,
                    AvatarKey = $"avatar_crime_{i % 5}",
                    Stats = new Stats(
                        Power: 50 + (i * 10),
                        Defense: 30 + (i * 5),
                        Agility: 40 + (i * 8),
                        Luck: 10 + i
                    ),
                    Energy = new Energy(Current: 100, Max: 100, RegenPerMinute: 5),
                    Rank = new Rank(RankPoints: 100 * (i + 1), Position: themeUsers.Length - i),
                    CreatedAtUtc = now,
                    LastEnergyCalcUtc = now
                };
                players.Add(player);
            }

            try {
                await _writeRepository.AddRangeAsync(players);
                await _writeRepository.SaveAsync();
            } catch { }

            return Unit.Value;
        }
    }
}
