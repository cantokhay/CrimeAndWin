namespace CrimeAndWin.Contracts.Events.Action;

public record CrimeCompletedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public Guid ActionId      { get; init; }
    public string ActionType  { get; init; } = string.Empty;
    public bool IsSuccess     { get; init; }
    public int ExpReward      { get; init; }
    public int EnergyCost     { get; init; }
    public decimal MoneyReward { get; init; }
    public Guid? ItemRewardId { get; init; }
    public DateTime OccurredAt { get; init; }
}
