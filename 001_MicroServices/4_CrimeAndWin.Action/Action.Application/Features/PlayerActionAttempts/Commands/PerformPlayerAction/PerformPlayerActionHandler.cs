using Action.Application.Abstract;
using Action.Domain.Entities;
using Action.Application.Mapping;
using CrimeAndWin.Contracts.Events.Action;
using CrimeAndWin.Action.GameMechanics;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace Action.Application.Features.PlayerActionAttempts.Commands.PerformPlayerAction
{
    public sealed class PerformPlayerActionHandler
        : IRequestHandler<PerformPlayerActionCommand, Guid>
    {
        private readonly IReadRepository<ActionDefinition> _actionRead;
        private readonly IReadRepository<PlayerActionAttempt> _attemptRead;
        private readonly IWriteRepository<PlayerActionAttempt> _attemptWrite;
        private readonly IReadRepository<PlayerEnergyState> _energyRead;
        private readonly IWriteRepository<PlayerEnergyState> _energyWrite;
        private readonly IPlayerProfileService _profileService;
        private readonly IGameSettingsService _gameSettings;
        private readonly SuccessRateCalculator _successCalculator;
        private readonly ActionMapper _mapper;
        private readonly IEventPublisher _publisher;
        private readonly Shared.Domain.Time.IDateTimeProvider _dateTimeProvider;
        public PerformPlayerActionHandler(
            IReadRepository<ActionDefinition> actionRead,
            IReadRepository<PlayerActionAttempt> attemptRead,
            IWriteRepository<PlayerActionAttempt> attemptWrite,
            IReadRepository<PlayerEnergyState> energyRead,
            IWriteRepository<PlayerEnergyState> energyWrite,
            IPlayerProfileService profileService,
            IGameSettingsService gameSettings,
            SuccessRateCalculator successCalculator,
            ActionMapper mapper,
            IEventPublisher publisher,
            Shared.Domain.Time.IDateTimeProvider dateTimeProvider)
        {
            _actionRead = actionRead;
            _attemptRead = attemptRead;
            _attemptWrite = attemptWrite;
            _energyRead = energyRead;
            _energyWrite = energyWrite;
            _profileService = profileService;
            _gameSettings = gameSettings;
            _successCalculator = successCalculator;
            _mapper = mapper;
            _publisher = publisher;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Guid> Handle(PerformPlayerActionCommand request, CancellationToken ct)
        {
            var now = _dateTimeProvider.UtcNow;

            // Definition Check
            var def = await _actionRead.GetByIdAsync(request.Request.ActionDefinitionId.ToString());
            if (def is null || !def.IsActive) throw new InvalidOperationException("Action not available.");

            // 1. Enerji kontrolü
            var energyState = await _energyRead.GetByIdAsync(request.Request.PlayerId.ToString());
            if (energyState == null)
            {
               int baseMaxEnergy = await _gameSettings.GetIntSettingAsync("BaseMaxEnergy", EnergyConstants.BaseMaxEnergy);

               // Initialize energy state if not exists
               energyState = new PlayerEnergyState
               {
                   Id = request.Request.PlayerId,
                   CurrentEnergy = baseMaxEnergy,
                   LastRefillAt = now,
                   CreatedAtUtc = now,
                   UpdatedAtUtc = now
               };
               await _energyWrite.AddAsync(energyState);
               await _energyWrite.SaveAsync();
            }

            if (energyState.CurrentEnergy < def.Requirements.EnergyCost)
                throw new InvalidOperationException("Insufficient energy.");

            // 2. Cooldown kontrolü
            var lastAttempt = await _attemptRead.Table
                                                .Where(a => a.PlayerId == request.Request.PlayerId && a.ActionDefinitionId == def.Id)
                                                .OrderByDescending(a => a.AttemptedAtUtc)
                                                .FirstOrDefaultAsync(ct);

            var cooldownCheck = CooldownManager.CheckCooldown(def.Code, lastAttempt?.AttemptedAtUtc, now);
            if (!cooldownCheck.IsReady)
                throw new InvalidOperationException($"Action on cooldown until {cooldownCheck.EndsAt}.");

            // 3. Başarı oranı hesapla
            var playerLevel = await _profileService.GetPlayerLevelAsync(request.Request.PlayerId);
            // Equipments - Skipping for now or mocking as empty
            var calcInput = new SuccessRateInput
            {
                PlayerLevel = playerLevel,
                Difficulty = (CrimeAndWin.Action.GameMechanics.ActionDifficulty)((def.Requirements.DifficultyLevel % 6) + 1), // Mapping for demonstration
                EquippedItems = new List<EquippedItem>()
            };
            var calcResult = _successCalculator.Calculate(calcInput);

            // 4. Sonucu uygula
            var attempt = _mapper.ToEntity(request.Request);
            attempt.Id = Guid.NewGuid();
            attempt.CorrelationId = Guid.NewGuid();
            attempt.AttemptedAtUtc = now;
            attempt.CreatedAtUtc = now;
            attempt.IsSuccess = calcResult.IsSuccess;
            attempt.SuccessRate = calcResult.FinalRate;
            attempt.CooldownEndsAt = CooldownManager.CalculateCooldownEnd(def.Code, now);

            // Deduct energy
            energyState.CurrentEnergy -= def.Requirements.EnergyCost;
            energyState.UpdatedAtUtc = now;

            await _attemptWrite.AddAsync(attempt);
            await _attemptWrite.SaveAsync();
            
            _energyWrite.Update(energyState);
            await _energyWrite.SaveAsync();

            await _publisher.PublishAsync(new CrimeCompletedEvent
            {
                CorrelationId = attempt.CorrelationId,
                PlayerId = attempt.PlayerId,
                ActionId = attempt.Id,
                ActionType = def.Code,
                IsSuccess = calcResult.IsSuccess,
                MoneyReward = calcResult.IsSuccess ? def.Rewards.MoneyGain : 0,
                ExpReward = calcResult.IsSuccess ? def.Rewards.PowerGain : 1, // small gain even on failure maybe?
                EnergyCost = def.Requirements.EnergyCost,
                ItemRewardId = (calcResult.IsSuccess && def.Rewards.ItemDrop) ? Guid.NewGuid() : null
            }, ct);

            return attempt.Id;
        }
    }
}


