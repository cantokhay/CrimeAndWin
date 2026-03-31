namespace CrimeAndWin.Contracts.Commands.PlayerProfile;

public record UpdatePlayerStatsCommand
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public int ExpDelta       { get; init; }   // + ekle, - çıkar
    public int EnergyDelta    { get; init; }   // + ekle, - çıkar
}

public record RevertPlayerStatsCommand
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public int ExpDelta       { get; init; }
    public int EnergyDelta    { get; init; }
}
