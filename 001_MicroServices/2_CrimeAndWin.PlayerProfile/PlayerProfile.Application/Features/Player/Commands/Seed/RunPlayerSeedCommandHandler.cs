using Bogus;
using MediatR;
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
            var faker = new Faker("tr");

            var players = new List<Domain.Entities.Player>();

            var rankPointsSet = new HashSet<int>();
            var rankPositionSet = new HashSet<int>();

            for (int i = 0; i < request.Count; i++)
            {
                int rankPoints;
                do { rankPoints = faker.Random.Int(100, 99999); } while (!rankPointsSet.Add(rankPoints));

                int rankPosition;
                do { rankPosition = faker.Random.Int(1, 10000); } while (!rankPositionSet.Add(rankPosition));

                var player = new Domain.Entities.Player
                {
                    Id = Guid.NewGuid(),
                    AppUserId = Guid.NewGuid(), // Identity service'den değil, fake seed
                    DisplayName = faker.Internet.UserName(),
                    AvatarKey = faker.PickRandom("avatar_knight", "avatar_mage", "avatar_thief", "avatar_rogue", "avatar_priest"),

                    Stats = new Stats(
                        Power: faker.Random.Int(10, 100),
                        Defense: faker.Random.Int(10, 100),
                        Agility: faker.Random.Int(10, 100),
                        Luck: faker.Random.Int(1, 50)
                    ),

                    Energy = new Energy(
                        Current: faker.Random.Int(10, 100),
                        Max: faker.Random.Int(100, 200),
                        RegenPerMinute: faker.Random.Int(1, 5)
                    ),

                    Rank = new Rank(rankPoints, rankPosition),

                    CreatedAtUtc = _dateTimeProvider.UtcNow,
                    UpdatedAtUtc = null,
                    IsDeleted = false,
                    LastEnergyCalcUtc = _dateTimeProvider.UtcNow
                };

                players.Add(player);
            }

            await _writeRepository.AddRangeAsync(players);
            await _writeRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
