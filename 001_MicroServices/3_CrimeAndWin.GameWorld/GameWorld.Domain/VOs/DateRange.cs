namespace GameWorld.Domain.VOs
{
    public sealed record DateRange(
        DateTime StartUtc,
        DateTime EndUtc
    );
}
