namespace CrimeAndWin.Contracts.Commands.PlayerProfile;

public record IncreasePlayerHeatCommand
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId { get; init; }
    public decimal Amount { get; init; }
    public string Reason { get; init; } = "Crime committed";
}
