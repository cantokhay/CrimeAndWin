using Bogus;
using Leadership.Domain.VOs;
using MediatR;
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

        public async Task<Unit> Handle(RunLeadershipSeedCommand request, CancellationToken cancellationToken)
        {
            var faker = new Faker("en");
            var leaderboards = new List<Domain.Entities.Leaderboard>();
            var entries = new List<Domain.Entities.LeaderboardEntry>();

            for (int i = 0; i < request.Count; i++)
            {
                var leaderboardId = Guid.NewGuid();

                var leaderboard = new Domain.Entities.Leaderboard
                {
                    Id = leaderboardId,
                    Name = faker.Company.CatchPhrase(),
                    Description = faker.Lorem.Sentence(),
                    GameWorldId = faker.Random.Bool(0.5f) ? Guid.NewGuid() : null,
                    SeasonId = faker.Random.Bool(0.5f) ? Guid.NewGuid() : null,
                    IsSeasonal = faker.Random.Bool(),
                    CreatedAtUtc = _clock.UtcNow,
                    IsDeleted = false
                };

                // 5–10 rastgele entry
                var entryCount = faker.Random.Int(5, 10);
                var usedPositions = new HashSet<int>();

                for (int j = 0; j < entryCount; j++)
                {
                    int position;
                    do { position = faker.Random.Int(1, 1000); } while (!usedPositions.Add(position));

                    var entry = new Domain.Entities.LeaderboardEntry
                    {
                        Id = Guid.NewGuid(),
                        LeaderboardId = leaderboardId,
                        PlayerId = Guid.NewGuid(),
                        Rank = new Rank
                        {
                            RankPoints = faker.Random.Int(100, 5000),
                            Position = position
                        },
                        IsActive = faker.Random.Bool(0.9f),
                        CreatedAtUtc = _clock.UtcNow,
                        IsDeleted = false
                    };

                    entries.Add(entry);
                }

                leaderboard.Entries = entries.Where(e => e.LeaderboardId == leaderboardId).ToList();
                leaderboards.Add(leaderboard);
            }

            await _leaderboardRepo.AddRangeAsync(leaderboards);
            await _entryRepo.AddRangeAsync(entries);
            await _leaderboardRepo.SaveAsync();
            await _entryRepo.SaveAsync();

            return Unit.Value;
        }
    }
}
