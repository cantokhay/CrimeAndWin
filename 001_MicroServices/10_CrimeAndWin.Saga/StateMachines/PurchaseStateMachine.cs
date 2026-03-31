using MassTransit;
using CrimeAndWin.Saga.States;
using CrimeAndWin.Contracts.Events.Economy;
using CrimeAndWin.Contracts.Events.Inventory;
using CrimeAndWin.Contracts.Commands.Economy;
using CrimeAndWin.Contracts.Commands.Inventory;

namespace CrimeAndWin.Saga.StateMachines;

public class PurchaseStateMachine : MassTransitStateMachine<PurchaseState>
{
    public State ProcessingPayment { get; private set; } = null!;
    public State ProcessingInventory { get; private set; } = null!;
    public State Completed { get; private set; } = null!;
    public State Failed { get; private set; } = null!;

    public Event<PurchaseInitiatedEvent> PurchaseInitiated { get; private set; } = null!;
    public Event<MoneyDeductedEvent> MoneyDeducted { get; private set; } = null!;
    public Event<InventoryGrantedEvent> InventoryGranted { get; private set; } = null!;

    public PurchaseStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => PurchaseInitiated, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => MoneyDeducted, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => InventoryGranted, x => x.CorrelateById(m => m.Message.CorrelationId));

        Initially(
            When(PurchaseInitiated)
                .Then(context =>
                {
                    context.Saga.PlayerId = context.Message.PlayerId;
                    context.Saga.ItemId = context.Message.ItemId;
                    context.Saga.Price = context.Message.Price;
                    context.Saga.Quantity = context.Message.Quantity;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .PublishAsync(context => context.Init<DeductMoneyCommand>(new
                {
                    CorrelationId = context.Saga.CorrelationId,
                    PlayerId = context.Saga.PlayerId,
                    Amount = context.Saga.Price,
                    Reason = "Item Purchase"
                }))
                .TransitionTo(ProcessingPayment)
        );

        During(ProcessingPayment,
            When(MoneyDeducted)
                .IfElse(context => context.Message.IsSuccess,
                    success => success
                        .Then(context => context.Saga.MoneyDeducted = true)
                        .PublishAsync(context => context.Init<GrantItemCommand>(new
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            PlayerId = context.Saga.PlayerId,
                            ItemId = context.Saga.ItemId,
                            Quantity = context.Saga.Quantity
                        }))
                        .TransitionTo(ProcessingInventory),
                    fail => fail
                        .Then(context => context.Saga.FailReason = context.Message.FailReason)
                        .TransitionTo(Failed)
                )
        );

        During(ProcessingInventory,
            When(InventoryGranted)
                .IfElse(context => context.Message.IsSuccess,
                    success => success.TransitionTo(Completed),
                    fail => fail
                        .Then(context => context.Saga.FailReason = context.Message.FailReason)
                        .PublishAsync(context => context.Init<RewardMoneyCommand>(new
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            PlayerId = context.Saga.PlayerId,
                            Amount = context.Saga.Price,
                            Reason = "Purchase Rollback"
                        }))
                        .TransitionTo(Failed)
                )
        );
    }
}
