using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Action.Infrastructure.Persistance.Context;
using Action.Application.Abstract;
using CrimeAndWin.Action.GameMechanics;
using CrimeAndWin.Contracts.Commands.PlayerProfile;

namespace CrimeAndWin.Action.BackgroundServices
{
    public class EnergyRefillBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<EnergyRefillBackgroundService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(60);

        public EnergyRefillBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<EnergyRefillBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Energy refill background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_checkInterval, stoppingToken);

                try
                {
                    await ProcessEnergyRefillsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Energy refill tick failed.");
                }
            }
        }

        private async Task ProcessEnergyRefillsAsync(CancellationToken ct)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ActionDbContext>();
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            var gameSettings = scope.ServiceProvider.GetRequiredService<IGameSettingsService>();

            DateTime now = DateTime.UtcNow;

            int refillInterval = await gameSettings.GetIntSettingAsync("RefillIntervalSeconds", EnergyConstants.RefillIntervalSeconds);
            int baseMaxEnergy = await gameSettings.GetIntSettingAsync("BaseMaxEnergy", EnergyConstants.BaseMaxEnergy);

            // Minimum interval check
            int minInterval = 30; 
            var candidates = await dbContext.PlayerEnergyStates
                .Where(p => EF.Functions.DateDiffSecond(p.LastRefillAt, now) >= minInterval)
                .ToListAsync(ct);

            int updatedCount = 0;

            foreach (var player in candidates)
            {
                int activeItemBonusSeconds = 0;
                
                if (!string.IsNullOrEmpty(player.ActiveBoostItem) && player.BoostExpiresAt.HasValue)
                {
                    if (now > player.BoostExpiresAt.Value)
                    {
                        // Boost expired
                        player.ActiveBoostItem = null;
                        player.BoostExpiresAt = null;
                        dbContext.PlayerEnergyStates.Update(player);
                    }
                    else
                    {
                        if (EnergyConstants.ItemRefillBonus.TryGetValue(player.ActiveBoostItem, out int bonus))
                        {
                            activeItemBonusSeconds = bonus;
                        }
                    }
                }



                int earned = EnergyCalculator.CalculateAccruedEnergy(
                    player.LastRefillAt,
                    now,
                    player.CurrentEnergy,
                    baseMaxEnergy,
                    activeItemBonusSeconds,
                    refillInterval);

                if (earned > 0)
                {
                    player.CurrentEnergy += earned;
                    player.LastRefillAt = now;
                    player.UpdatedAtUtc = now;
                    dbContext.PlayerEnergyStates.Update(player);

                    await publishEndpoint.Publish(new UpdatePlayerStatsCommand
                    {
                        CorrelationId = Guid.NewGuid(),
                        PlayerId = player.Id,
                        EnergyDelta = earned,
                        ExpDelta = 0
                    }, ct);

                    updatedCount++;
                }
            }

            if (updatedCount > 0 || dbContext.ChangeTracker.HasChanges())
            {
                await dbContext.SaveChangesAsync(ct);
                _logger.LogInformation("Refilled energy for {Count} players.", updatedCount);
            }
        }
    }
}

