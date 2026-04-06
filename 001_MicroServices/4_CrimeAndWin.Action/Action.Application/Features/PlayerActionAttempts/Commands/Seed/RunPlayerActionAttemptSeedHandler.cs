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

        public async Task<Unit> Handle(RunPlayerActionAttemptSeedCommand request, CancellationToken ct)
        {
            var now = _clock.UtcNow;
            var themeUsers = new[] { "Boss", "Hitman", "Mole", "Fixer", "Dealer", "Enforcer", "Launderer" };
            var definitions = _actionDefRepo.GetAll(tracking: false).ToList();
            
            if (!definitions.Any()) return Unit.Value;

            var attempts = new List<PlayerActionAttempt>();

            for (int i = 0; i < themeUsers.Length; i++)
            {
                var playerId = Guid.Parse($"22222222-2222-2222-2222-{i:D12}"); // From PlayerProfile
                
                // Add 3 attempts for each player
                for (int j = 0; j < 3; j++)
                {
                    var def = definitions[(i + j) % definitions.Count];
                    var isSuccess = (i + j) % 2 == 0;
                    
                    attempts.Add(new PlayerActionAttempt
                    {
                        Id = Guid.NewGuid(),
                        CorrelationId = Guid.NewGuid(),
                        PlayerId = playerId,
                        ActionDefinitionId = def.Id,
                        AttemptedAtUtc = now.AddHours(-1 * (j + 1)),
                        PlayerActionResults = new PlayerActionResult(isSuccess ? 1.0 : 0.0, isSuccess ? OutcomeType.Success : OutcomeType.Fail),
                        CooldownEndsAt = now.AddMinutes(30),
                        IsSuccess = isSuccess,
                        SuccessRate = isSuccess ? 0.95 : 0.2,
                        CreatedAtUtc = now
                    });
                }
            }

            try {
                await _attemptRepo.AddRangeAsync(attempts);
                await _attemptRepo.SaveAsync();
            } catch { }

            return Unit.Value;
        }
    }
}
