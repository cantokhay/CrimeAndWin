using System;

namespace CrimeAndWin.Action.GameMechanics;

public static class EnergyCalculator
{
    /// <summary>
    /// Oyuncunun seviyesine göre max enerji kapasitesini hesaplar.
    /// </summary>
    public static int GetMaxEnergy(int playerLevel)
    {
        return EnergyConstants.BaseMaxEnergy + (playerLevel * EnergyConstants.EnergyPerLevel);
    }

    /// <summary>
    /// Son yenileme zamanından bu yana kazanılması gereken enerji miktarını hesaplar.
    /// Item hızlandırması varsa intervalden düşer.
    /// </summary>
    public static int CalculateAccruedEnergy(
        DateTime lastRefillAt,
        DateTime now,
        int currentEnergy,
        int maxEnergy,
        int activeItemBonusSeconds = 0,
        int? refillIntervalSeconds = null)
    {
        if (currentEnergy >= maxEnergy) return 0;

        int interval = refillIntervalSeconds ?? EnergyConstants.RefillIntervalSeconds;

        int effectiveInterval = Math.Max(
            interval - activeItemBonusSeconds,
            30 // minimum 30 saniye — item bonusu ne kadar güçlü olursa olsun
        );

        double elapsedSeconds = (now - lastRefillAt).TotalSeconds;
        int ticksEarned       = (int)(elapsedSeconds / effectiveInterval);
        int energyEarned      = ticksEarned * EnergyConstants.RefillAmountPerTick;

        return Math.Min(energyEarned, maxEnergy - currentEnergy);
    }

    /// <summary>
    /// Bir sonraki enerji tick'ine kalan süreyi hesaplar.
    /// </summary>
    public static TimeSpan TimeUntilNextRefill(
        DateTime lastRefillAt,
        DateTime now,
        int activeItemBonusSeconds = 0)
    {
        int effectiveInterval = Math.Max(
            EnergyConstants.RefillIntervalSeconds - activeItemBonusSeconds,
            30
        );

        double elapsedSeconds   = (now - lastRefillAt).TotalSeconds;
        double remainingSeconds = effectiveInterval - (elapsedSeconds % effectiveInterval);

        return TimeSpan.FromSeconds(remainingSeconds);
    }
}


