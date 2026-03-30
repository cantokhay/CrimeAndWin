using Action.Domain.Entities;
using Action.Domain.VOs;
using Bogus;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Action.Application.Features.ActionDefinitons.Commands.Seed
{
    public sealed class RunActionSeedHandler : IRequestHandler<RunActionSeedCommand, Unit>
    {
        private readonly IWriteRepository<ActionDefinition> _actionRepo;
        private readonly IDateTimeProvider _clock;

        public RunActionSeedHandler(IWriteRepository<ActionDefinition> actionRepo, IDateTimeProvider clock)
        {
            _actionRepo = actionRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(RunActionSeedCommand request, CancellationToken cancellationToken)
        {
            var faker = new Faker("en");

            var definitions = new List<ActionDefinition>();

            for (int i = 0; i < request.Count; i++)
            {
                // benzersiz code oluştur
                var code = faker.Hacker.Verb().Replace(" ", "_") + "_" + faker.Random.AlphaNumeric(5).ToUpper();

                var def = new ActionDefinition
                {
                    Id = Guid.NewGuid(),
                    Code = code,
                    DisplayName = faker.Hacker.IngVerb() + " " + faker.Hacker.Noun(),
                    Description = faker.Lorem.Sentence(),
                    Requirements = new ActionRequirements(
                        MinPower: faker.Random.Int(10, 100),
                        EnergyCost: faker.Random.Int(5, 30)
                    ),
                    Rewards = new ActionRewards(
                        PowerGain: faker.Random.Int(1, 10),
                        ItemDrop: faker.Random.Bool(0.3f), // %30 drop şansı
                        MoneyGain: Math.Round(faker.Random.Decimal(10, 500), 2)
                    ),
                    IsActive = faker.Random.Bool(0.85f), // %85 aktif
                    CreatedAtUtc = _clock.UtcNow,
                    IsDeleted = false
                };

                definitions.Add(def);
            }

            await _actionRepo.AddRangeAsync(definitions);
            await _actionRepo.SaveAsync();

            return Unit.Value;
        }
    }
}
