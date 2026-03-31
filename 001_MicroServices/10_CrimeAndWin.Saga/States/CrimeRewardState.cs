using MassTransit;

namespace CrimeAndWin.Saga.States;

public class CrimeRewardState : SagaStateMachineInstance
{
    public Guid CorrelationId    { get; set; }
    public string CurrentState   { get; set; } = string.Empty;
    public Guid PlayerId         { get; set; }
    public Guid ActionId         { get; set; }
    public decimal MoneyReward   { get; set; }
    public int ExpReward         { get; set; }
    public int EnergyCost        { get; set; }
    public Guid? ItemRewardId    { get; set; }
    public bool EconomyDone      { get; set; }
    public bool InventoryDone    { get; set; }
    public bool ProfileDone      { get; set; }
    public DateTime CreatedAt    { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? FailReason    { get; set; }
}
