using MassTransit;

namespace CrimeAndWin.Saga.States;

public class CrimeActionState : SagaStateMachineInstance
{
    public Guid CorrelationId  { get; set; }
    public string CurrentState { get; set; } = string.Empty;
    public Guid PlayerId       { get; set; }
    public Guid ActionId       { get; set; }
    public int ExpDelta        { get; set; }
    public int EnergyDelta     { get; set; }
    public DateTime CreatedAt  { get; set; }
    public string? FailReason  { get; set; }
}


