namespace CrimeAndWin.Contracts.Events.Economy;

public record LaunderingStartedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId { get; init; }
    public decimal AmountToLaunder { get; init; }
    public decimal EfficiencyRate { get; init; } = 0.85m; // Current business efficiency
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}

public record LaunderingCompletedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId { get; init; }
    public decimal FinalCleanAmount { get; init; }
    public decimal BurnedAmount { get; init; }
}
