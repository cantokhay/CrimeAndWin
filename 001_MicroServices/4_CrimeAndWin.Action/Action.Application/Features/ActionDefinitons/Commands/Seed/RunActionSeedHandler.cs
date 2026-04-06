using Action.Domain.Entities;
using Action.Domain.VOs;
using Bogus;
using Shared.Application.Abstractions.Messaging;
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

        public async Task<Unit> Handle(RunActionSeedCommand request, CancellationToken ct)
        {
            var now = _clock.UtcNow;
            
            var crimes = new List<(string Code, string Name, string Desc, int Power, int Energy, decimal Money)>
            {
                ("STREET_PICKPOCKET", "Pickpocket", "Steal a wallet from a tourist.", 10, 5, 50.0m),
                ("STREET_MUGGING", "Street Mugging", "Demand cash from a passerby.", 20, 10, 150.0m),
                ("UNDERGROUND_BURGLARY", "House Burglary", "Break into a safe neighborhood house.", 50, 25, 800.0m),
                ("UNDERGROUND_CAR_THEFT", "Car Theft", "Steal a luxury vehicle.", 40, 20, 600.0m),
                ("HEIST_BANK", "Bank Robbery", "High-risk bank vault heist.", 150, 60, 5000.0m),
                ("HEIST_CASINO", "Casino Heist", "Rob the Diamond Casino vault.", 200, 80, 12000.0m)
            };

            var defs = crimes.Select((c, i) => new ActionDefinition
            {
                Id = Guid.Parse($"33333333-3333-3333-3333-{i:D12}"),
                Code = c.Code,
                DisplayName = c.Name,
                Description = c.Desc,
                Requirements = new ActionRequirements(MinPower: c.Power, EnergyCost: c.Energy),
                Rewards = new ActionRewards(
                    PowerGain: i + 1, 
                    ItemDrop: i > 2, 
                    MoneyGain: c.Money
                ),
                IsActive = true,
                CreatedAtUtc = now
            }).ToList();

            try {
                await _actionRepo.AddRangeAsync(defs);
                await _actionRepo.SaveAsync();
            } catch { }

            return Unit.Value;
        }
    }
}
