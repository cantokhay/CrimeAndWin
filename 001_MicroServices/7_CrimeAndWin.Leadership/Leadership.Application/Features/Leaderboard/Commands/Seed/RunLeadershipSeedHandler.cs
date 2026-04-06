using Bogus;
using Leadership.Domain.VOs;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Leadership.Application.Features.Leaderboard.Commands.Seed
{
    public sealed class RunLeadershipSeedHandler : IRequestHandler<RunLeadershipSeedCommand, Unit>
    {
        private readonly IWriteRepository<Domain.Entities.Leaderboard> _leaderboardRepo;
        private readonly IWriteRepository<Domain.Entities.LeaderboardEntry> _entryRepo;
        private readonly IDateTimeProvider _clock;

        public RunLeadershipSeedHandler(
            IWriteRepository<Domain.Entities.Leaderboard> leaderboardRepo,
            IWriteRepository<Domain.Entities.LeaderboardEntry> entryRepo,
            IDateTimeProvider clock)
        {
            _leaderboardRepo = leaderboardRepo;
            _entryRepo = entryRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(RunLeadershipSeedCommand request, CancellationToken ct)
        {
            var now = _clock.UtcNow;
            var themeUsers = new[] { "Boss", "Hitman", "Mole", "Fixer", "Dealer", "Enforcer", "Launderer" };
            
            var boards = new[] { "Global Top Crime Leaders", "Weekly Street Hustle", "Season 1 Most Wanted" };
            
            for (int i = 0; i < boards.Length; i++)
            {
                var lbId = Guid.Parse($"66666666-6666-6666-6666-{i:D12}");
                var lb = new Domain.Entities.Leaderboard
                {
                    Id = lbId,
                    Name = boards[i],
                    Description = $"The most notorious players in the {boards[i]} category.",
                    IsSeasonal = i == 2,
                    CreatedAtUtc = now
                };

                var entries = new List<Domain.Entities.LeaderboardEntry>();
                for (int j = 0; j < themeUsers.Length; j++)
                {
                    var playerId = Guid.Parse($"22222222-2222-2222-2222-{j:D12}");
                    entries.Add(new Domain.Entities.LeaderboardEntry
                    {
                        Id = Guid.NewGuid(),
                        LeaderboardId = lbId,
                        PlayerId = playerId,
                        Rank = new Rank { RankPoints = 1000 - (j * 100), Position = j + 1 },
                        IsActive = true,
                        CreatedAtUtc = now
                    });
                }

                try {
                    await _leaderboardRepo.AddAsync(lb);
                    await _entryRepo.AddRangeAsync(entries);
                    await _leaderboardRepo.SaveAsync();
                    await _entryRepo.SaveAsync();
                } catch { }
            }

            return Unit.Value;
        }
    }
}
