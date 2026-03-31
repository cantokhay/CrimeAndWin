namespace CrimeAndWin.Contracts.Events.Leadership;

public record RankChangedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public int NewRank        { get; init; }
    public int OldRank        { get; init; }
    public DateTime OccurredAt { get; init; }
}
