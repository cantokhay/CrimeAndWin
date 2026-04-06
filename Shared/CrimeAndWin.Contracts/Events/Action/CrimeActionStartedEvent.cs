namespace CrimeAndWin.Contracts.Events.Action;

public record CrimeActionStartedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId { get; init; }
    public Guid ActionId { get; init; }
    public string Area { get; init; } = "Downtown";
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
