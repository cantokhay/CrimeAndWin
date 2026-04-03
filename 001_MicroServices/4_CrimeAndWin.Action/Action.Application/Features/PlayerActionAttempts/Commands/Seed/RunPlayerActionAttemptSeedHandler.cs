using Action.Domain.Entities;
using Action.Domain.Enums;
using Action.Domain.VOs;
using Bogus;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Action.Application.Features.PlayerActionAttempts.Commands.Seed
{
    public sealed class RunPlayerActionAttemptSeedHandler : IRequestHandler<RunPlayerActionAttemptSeedCommand, Unit>
    {
        private readonly IWriteRepository<PlayerActionAttempt> _attemptRepo;
        private readonly IReadRepository<ActionDefinition> _actionDefRepo;
        private readonly IDateTimeProvider _clock;

        public RunPlayerActionAttemptSeedHandler(
            IWriteRepository<PlayerActionAttempt> attemptRepo,
            IReadRepository<ActionDefinition> actionDefRepo,
            IDateTimeProvider clock)
        {
            _attemptRepo = attemptRepo;
            _actionDefRepo = actionDefRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(RunPlayerActionAttemptSeedCommand request, CancellationToken cancellationToken)
        {
            var faker = new Faker("en");

            // ActionDefinition’lar olmalý ki denemelere baðlanabilsin
            var definitions = _actionDefRepo.GetAll(tracking: false).ToList();
            if (!definitions.Any())
                throw new InvalidOperationException("Seed yapýlacak ActionDefinition bulunamadý.");

            var attempts = new List<PlayerActionAttempt>();

            for (int i = 0; i < request.Count; i++)
            {
                var definition = faker.PickRandom(definitions);

                // %60 baþarý, %40 baþarýsýz rastgele sonuç
                var successRate = faker.Random.Double(0.2, 1.0);
                var outcome = successRate > 0.6 ? OutcomeType.Success : OutcomeType.Fail;

                var attempt = new PlayerActionAttempt
                {
                    Id = Guid.NewGuid(),
                    PlayerId = Guid.NewGuid(), // fake player id
                    ActionDefinitionId = definition.Id,
                    AttemptedAtUtc = _clock.UtcNow.AddMinutes(-faker.Random.Int(1, 300)),
                    PlayerActionResults = new PlayerActionResult(successRate, outcome),
                    CreatedAtUtc = _clock.UtcNow,
                    IsDeleted = false
                };

                attempts.Add(attempt);
            }

            await _attemptRepo.AddRangeAsync(attempts);
            await _attemptRepo.SaveAsync();

            return Unit.Value;
        }
    }
}


