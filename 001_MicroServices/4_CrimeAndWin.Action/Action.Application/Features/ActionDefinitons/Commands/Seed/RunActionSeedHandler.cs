using Action.Domain.Entities;
using Action.Domain.VOs;
using Action.Domain.Enums;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Action.Application.Features.ActionDefinitons.Commands.Seed
{
    public sealed class RunActionSeedHandler : IRequestHandler<RunActionSeedCommand, Unit>
    {
        private readonly IWriteRepository<Action.Domain.Entities.ActionDefinition> _actionWrite;
        private readonly IReadRepository<Action.Domain.Entities.ActionDefinition> _actionRead;

        public RunActionSeedHandler(
            IWriteRepository<Action.Domain.Entities.ActionDefinition> actionWrite,
            IReadRepository<Action.Domain.Entities.ActionDefinition> actionRead)
        {
            _actionWrite = actionWrite;
            _actionRead = actionRead;
        }

        public async Task<Unit> Handle(RunActionSeedCommand request, CancellationToken ct)
        {
            var seedDate = SeedDataConstants.SeedDate;
            
            var crimes = new List<(string Code, string Name, ActionType Type, decimal Success, int Power, int Energy, decimal Money)>
            {
                ("STREET_PICKPOCKET", "Pickpocket", ActionType.Crime, 90m, 10, 5, 50.0m),
                ("STREET_MUGGING", "Street Mugging", ActionType.Crime, 80m, 20, 10, 150.0m),
                ("UNDERGROUND_BURGLARY", "House Burglary", ActionType.Crime, 60m, 50, 25, 800.0m),
                ("UNDERGROUND_CAR_THEFT", "Car Theft", ActionType.Crime, 70m, 40, 20, 600.0m),
                ("HEIST_BANK", "Bank Robbery", ActionType.Crime, 30m, 150, 60, 5000.0m),
                ("BRIBEY_POLICE", "Bribe Policeman", ActionType.Bribe, 50m, 0, 5, 500.0m)
            };

            var defs = crimes.Select((c, i) => new Action.Domain.Entities.ActionDefinition
            {
                Id = Guid.Parse($"d0000000-0000-0000-0000-{i:D12}"),
                Code = c.Code,
                DisplayName = c.Name,
                Description = $"{c.Name} action.",
                Type = c.Type,
                BaseSuccessRate = c.Success,
                HeatImpact = c.Type == ActionType.Crime ? 10 : -10,
                RespectImpact = c.Type == ActionType.Crime ? 5 : 2,
                Requirements = new ActionRequirements(MinPower: c.Power, EnergyCost: c.Energy),
                Rewards = new ActionRewards(
                    PowerGain: i + 1, 
                    ItemDrop: i > 2, 
                    MoneyGain: c.Money
                ),
                IsActive = true,
                CreatedAtUtc = seedDate
            }).ToList();

            foreach (var def in defs)
            {
                if (await _actionRead.GetByIdAsync(def.Id.ToString()) == null)
                {
                    await _actionWrite.AddAsync(def);
                }
            }
            await _actionWrite.SaveAsync();

            return Unit.Value;
        }
    }
}
