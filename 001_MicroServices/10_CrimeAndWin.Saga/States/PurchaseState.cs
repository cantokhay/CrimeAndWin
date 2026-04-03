using MassTransit;

namespace CrimeAndWin.Saga.States;

public class PurchaseState : SagaStateMachineInstance
{
    public Guid CorrelationId    { get; set; }
    public string CurrentState   { get; set; } = string.Empty;
    public Guid PlayerId         { get; set; }
    public Guid ItemId           { get; set; }
    public decimal Price         { get; set; }
    public int Quantity          { get; set; }
    public bool MoneyDeducted    { get; set; }
    public DateTime CreatedAt    { get; set; }
    public string? FailReason    { get; set; }
}


