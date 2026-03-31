namespace CrimeAndWin.Contracts.Events.Inventory;

public record InventoryGrantedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public bool IsSuccess     { get; init; }
    public string? FailReason { get; init; }
}
