using MassTransit;

namespace CrimeAndWin.Saga.States;

public class RankUpdateState : SagaStateMachineInstance
{
    public Guid CorrelationId  { get; set; }
    public string CurrentState { get; set; } = string.Empty;
    public Guid PlayerId       { get; set; }
    public int NewRank         { get; set; }
    public int OldRank         { get; set; }
    public DateTime CreatedAt  { get; set; }
    public string? FailReason  { get; set; }
}


