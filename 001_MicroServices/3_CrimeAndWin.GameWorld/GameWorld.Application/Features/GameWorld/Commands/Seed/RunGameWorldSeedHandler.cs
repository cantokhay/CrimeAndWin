using Bogus;
using GameWorld.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace GameWorld.Application.Features.GameWorld.Commands.Seed
{
    public sealed class RunGameWorldSeedHandler : IRequestHandler<RunGameWorldSeedCommand, Unit>
    {
        private readonly IWriteRepository<Domain.Entities.GameWorld> _worldRepo;
        private readonly IWriteRepository<Domain.Entities.Season> _seasonRepo;
        private readonly IDateTimeProvider _clock;

        public RunGameWorldSeedHandler(
            IWriteRepository<Domain.Entities.GameWorld> worldRepo,
            IWriteRepository<Domain.Entities.Season> seasonRepo,
            IDateTimeProvider clock)
        {
            _worldRepo = worldRepo;
            _seasonRepo = seasonRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(RunGameWorldSeedCommand request, CancellationToken cancellationToken)
        {
            var faker = new Faker("tr");

            var gameWorlds = new List<Domain.Entities.GameWorld>();
            var seasons = new List<Domain.Entities.Season>();

            for (int i = 1; i <= request.Count; i++)
            {
                var world = new Domain.Entities.GameWorld
                {
                    Id = Guid.NewGuid(),
                    Name = faker.Company.CatchPhrase(),
                    Rule = new GameRule(
                        MaxEnergy: faker.Random.Int(100, 500),
                        RegenRatePerHour: faker.Random.Int(10, 50)
                    ),
                    CreatedAtUtc = _clock.UtcNow,
                    IsDeleted = false,
                    Seasons = new List<Domain.Entities.Season>()
                };

                // Her dünyaya 1 aktif, 1 geçmiş sezon ekle
                var currentSeason = new Domain.Entities.Season
                {
                    Id = Guid.NewGuid(),
                    GameWorldId = world.Id,
                    SeasonNumber = 2,
                    DateRange = new DateRange(
                        StartUtc: _clock.UtcNow.AddDays(-10),
                        EndUtc: _clock.UtcNow.AddDays(20)
                    ),
                    IsActive = true,
                    CreatedAtUtc = _clock.UtcNow,
                    IsDeleted = false
                };

                var pastSeason = new Domain.Entities.Season
                {
                    Id = Guid.NewGuid(),
                    GameWorldId = world.Id,
                    SeasonNumber = 1,
                    DateRange = new DateRange(
                        StartUtc: _clock.UtcNow.AddDays(-60),
                        EndUtc: _clock.UtcNow.AddDays(-30)
                    ),
                    IsActive = false,
                    CreatedAtUtc = _clock.UtcNow,
                    IsDeleted = false
                };

                world.Seasons.Add(currentSeason);
                world.Seasons.Add(pastSeason);

                gameWorlds.Add(world);
                seasons.AddRange(new[] { currentSeason, pastSeason });
            }

            await _worldRepo.AddRangeAsync(gameWorlds);
            await _seasonRepo.AddRangeAsync(seasons);

            await _worldRepo.SaveAsync();
            await _seasonRepo.SaveAsync();

            return Unit.Value;
        }
    }
}
