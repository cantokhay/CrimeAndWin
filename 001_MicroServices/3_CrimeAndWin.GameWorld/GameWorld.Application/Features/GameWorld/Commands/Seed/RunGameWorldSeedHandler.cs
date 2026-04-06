using GameWorld.Domain.VOs;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace GameWorld.Application.Features.GameWorld.Commands.Seed
{
    public sealed class RunGameWorldSeedHandler : IRequestHandler<RunGameWorldSeedCommand, Unit>
    {
        private readonly IWriteRepository<global::GameWorld.Domain.Entities.GameWorld> _worldWrite;
        private readonly IReadRepository<global::GameWorld.Domain.Entities.GameWorld> _worldRead;
        private readonly IWriteRepository<global::GameWorld.Domain.Entities.Season> _seasonWrite;
        private readonly IReadRepository<global::GameWorld.Domain.Entities.Season> _seasonRead;

        public RunGameWorldSeedHandler(
            IWriteRepository<global::GameWorld.Domain.Entities.GameWorld> worldWrite,
            IReadRepository<global::GameWorld.Domain.Entities.GameWorld> worldRead,
            IWriteRepository<global::GameWorld.Domain.Entities.Season> seasonWrite,
            IReadRepository<global::GameWorld.Domain.Entities.Season> seasonRead)
        {
            _worldWrite = worldWrite;
            _worldRead = worldRead;
            _seasonWrite = seasonWrite;
            _seasonRead = seasonRead;
        }

        public async Task<Unit> Handle(RunGameWorldSeedCommand request, CancellationToken cancellationToken)
        {
            var seedDate = SeedDataConstants.SeedDate;

            // 1. Official Game World
            var worldId = SeedDataConstants.MainlandWorldId;
            if (await _worldRead.GetByIdAsync(worldId.ToString()) == null)
            {
                var world = new global::GameWorld.Domain.Entities.GameWorld
                {
                    Id = worldId,
                    Name = "Crime & Win Mainland",
                    Rule = new GameRule(MaxEnergy: 100, RegenRatePerHour: 20),
                    CreatedAtUtc = seedDate,
                    IsDeleted = false
                };
                await _worldWrite.AddAsync(world);
                await _worldWrite.SaveAsync();
            }

            // 2. Official Season
            var seasonId = SeedDataConstants.SeasonOneId;
            if (await _seasonRead.GetByIdAsync(seasonId.ToString()) == null)
            {
                var season = new global::GameWorld.Domain.Entities.Season
                {
                    Id = seasonId,
                    GameWorldId = worldId,
                    SeasonNumber = 1,
                    DateRange = new DateRange(
                        StartUtc: seedDate,
                        EndUtc: seedDate.AddMonths(3)
                    ),
                    IsActive = true,
                    CreatedAtUtc = seedDate,
                    IsDeleted = false
                };
                await _seasonWrite.AddAsync(season);
                await _seasonWrite.SaveAsync();
            }

            return Unit.Value;
        }
    }
}
