namespace PlayerProfile.Domain.VOs
{
    public readonly record struct Energy(
        int Current,
        int Max,
        int RegenPerMinute
        );
}
