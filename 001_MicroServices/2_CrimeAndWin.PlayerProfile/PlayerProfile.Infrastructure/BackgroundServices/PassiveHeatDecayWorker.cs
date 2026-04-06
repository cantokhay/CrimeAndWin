using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using PlayerProfile.Infrastructure.Persistance.Context;

namespace PlayerProfile.Infrastructure.BackgroundServices
{
    public class PassiveHeatDecayWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PassiveHeatDecayWorker> _logger;
        private const int IntervalMinutes = 10;
        private const decimal DecayPercentage = 0.02m; // %2 Reduction

        public PassiveHeatDecayWorker(IServiceProvider serviceProvider, ILogger<PassiveHeatDecayWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Passive Heat Decay Worker is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(IntervalMinutes), stoppingToken);
                    await DecayHeatAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during Heat Decay execution.");
                }
            }

            _logger.LogInformation("Passive Heat Decay Worker is stopping.");
        }

        private async Task DecayHeatAsync(CancellationToken ct)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PlayerProfileDbContext>();

            // Perform Bulk Update: Reduce HeatIndex by 2% for everyone with Heat > 0
            // Success formula: CurrentHeat = CurrentHeat * 0.98. If < 0.1, set to 0.
            int rowsAffected = await context.Players
                .Where(p => p.HeatIndex > 0 && !p.IsDeleted)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.HeatIndex, p => p.HeatIndex * (1 - DecayPercentage)), ct);

            if (rowsAffected > 0)
            {
                _logger.LogInformation("Passive Heat Decay applied to {Count} players.", rowsAffected);
            }
        }
    }
}
