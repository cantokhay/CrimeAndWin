using System;

namespace CrimeAndWin.Action.GameMechanics;

public static class CooldownManager
{
    /// <summary>
    /// Belirtilen aksiyon tipinin cooldown süresini saniye olarak döner.
    /// </summary>
    public static int GetCooldownSeconds(string actionType)
    {
        return CooldownConstants.CooldownByActionType.TryGetValue(actionType, out int seconds)
            ? seconds
            : CooldownConstants.DefaultCooldownSeconds;
    }

    /// <summary>
    /// Oyuncunun bu aksiyonu şu an yapıp yapamayacağını kontrol eder.
    /// </summary>
    public static CooldownCheckResult CheckCooldown(
        string actionType,
        DateTime? lastActionAt,
        DateTime now)
    {
        if (lastActionAt is null)
            return CooldownCheckResult.Ready();

        int cooldownSeconds     = GetCooldownSeconds(actionType);
        DateTime cooldownEndsAt = lastActionAt.Value.AddSeconds(cooldownSeconds);

        if (now >= cooldownEndsAt)
            return CooldownCheckResult.Ready();

        TimeSpan remaining = cooldownEndsAt - now;
        return CooldownCheckResult.NotReady(remaining, cooldownEndsAt);
    }

    /// <summary>
    /// Aksiyonun cooldown bitiş zamanını hesaplar (aksiyon sonrası kaydedilecek).
    /// </summary>
    public static DateTime CalculateCooldownEnd(string actionType, DateTime actionTime)
    {
        return actionTime.AddSeconds(GetCooldownSeconds(actionType));
    }
}

public record CooldownCheckResult
{
    public bool IsReady          { get; init; }
    public TimeSpan? Remaining   { get; init; }
    public DateTime? EndsAt      { get; init; }

    public static CooldownCheckResult Ready() =>
        new() { IsReady = true };

    public static CooldownCheckResult NotReady(TimeSpan remaining, DateTime endsAt) =>
        new() { IsReady = false, Remaining = remaining, EndsAt = endsAt };
}

