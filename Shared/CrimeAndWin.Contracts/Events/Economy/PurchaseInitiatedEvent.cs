namespace CrimeAndWin.Contracts.Events.Economy;

public record PurchaseInitiatedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public Guid ItemId        { get; init; }
    public decimal Price      { get; init; }
    public int Quantity       { get; init; } = 1;
    public DateTime OccurredAt { get; init; }
}

public record MoneyDeductedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public bool IsSuccess     { get; init; }
    public string? FailReason { get; init; }
}
