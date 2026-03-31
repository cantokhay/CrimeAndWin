namespace CrimeAndWin.Contracts.Events.Economy;

public record EconomyRewardedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public bool IsSuccess     { get; init; }
    public string? FailReason { get; init; }
}
