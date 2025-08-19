namespace GameWorld.Domain.VOs
{
    public sealed record GameRule(
        int MaxEnergy,
        int RegenRatePerHour
    );
}
