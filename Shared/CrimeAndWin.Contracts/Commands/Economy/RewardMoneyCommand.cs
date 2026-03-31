namespace CrimeAndWin.Contracts.Commands.Economy;

public record RewardMoneyCommand
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public decimal Amount     { get; init; }
    public string Reason      { get; init; } = string.Empty;
}

public record DeductMoneyCommand
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public decimal Amount     { get; init; }
    public string Reason      { get; init; } = string.Empty;
}
