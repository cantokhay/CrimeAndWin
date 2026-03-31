namespace CrimeAndWin.Contracts.Commands.Inventory;

public record GrantItemCommand
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public Guid ItemId        { get; init; }
    public int Quantity       { get; init; } = 1;
}

public record RevokeItemCommand
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public Guid ItemId        { get; init; }
    public int Quantity       { get; init; } = 1;
}
