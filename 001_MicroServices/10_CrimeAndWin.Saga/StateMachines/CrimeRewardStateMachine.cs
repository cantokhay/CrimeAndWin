using MassTransit;
using CrimeAndWin.Saga.States;
using CrimeAndWin.Contracts.Events.Action;
using CrimeAndWin.Contracts.Events.Economy;
using CrimeAndWin.Contracts.Events.Inventory;
using CrimeAndWin.Contracts.Events.PlayerProfile;
using CrimeAndWin.Contracts.Commands.Economy;
using CrimeAndWin.Contracts.Commands.Inventory;
using CrimeAndWin.Contracts.Commands.PlayerProfile;

namespace CrimeAndWin.Saga.StateMachines;

public class CrimeRewardStateMachine : MassTransitStateMachine<CrimeRewardState>
{
    public State ProcessingReward { get; private set; } = null!;
    public State WaitingForEconomy { get; private set; } = null!;
    public State WaitingForInventory { get; private set; } = null!;
    public State WaitingForProfile { get; private set; } = null!;
    public State Failed { get; private set; } = null!;
    public State Completed { get; private set; } = null!;

    public Event<CrimeCompletedEvent> CrimeCompleted { get; private set; } = null!;
    public Event<EconomyRewardedEvent> EconomyRewarded { get; private set; } = null!;
    public Event<InventoryGrantedEvent> InventoryGranted { get; private set; } = null!;
    public Event<PlayerStatsUpdatedEvent> PlayerStatsUpdated { get; private set; } = null!;

    public CrimeRewardStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CrimeCompleted, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => EconomyRewarded, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => InventoryGranted, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => PlayerStatsUpdated, x => x.CorrelateById(m => m.Message.CorrelationId));

        Initially(
            When(CrimeCompleted)
                .Then(context =>
                {
                    context.Saga.PlayerId = context.Message.PlayerId;
                    context.Saga.ActionId = context.Message.ActionId;
                    context.Saga.MoneyReward = context.Message.MoneyReward;
                    context.Saga.ExpReward = context.Message.ExpReward;
                    context.Saga.EnergyCost = context.Message.EnergyCost;
                    context.Saga.ItemRewardId = context.Message.ItemRewardId;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .PublishAsync(context => context.Init<RewardMoneyCommand>(new
                {
                    CorrelationId = context.Saga.CorrelationId,
                    PlayerId = context.Saga.PlayerId,
                    Amount = context.Saga.MoneyReward,
                    Reason = "Crime Reward"
                }))
                .TransitionTo(WaitingForEconomy)
        );

        During(WaitingForEconomy,
            When(EconomyRewarded)
                .IfElse(context => context.Message.IsSuccess,
                    success => success
                        .Then(context => context.Saga.EconomyDone = true)
                        .IfElse(context => context.Saga.ItemRewardId.HasValue,
                            hasItem => hasItem
                                .PublishAsync(context => context.Init<GrantItemCommand>(new
                                {
                                    CorrelationId = context.Saga.CorrelationId,
                                    PlayerId = context.Saga.PlayerId,
                                    ItemId = context.Saga.ItemRewardId!.Value,
                                    Quantity = 1
                                }))
                                .TransitionTo(WaitingForInventory),
                            noItem => noItem
                                .PublishAsync(context => context.Init<UpdatePlayerStatsCommand>(new
                                {
                                    CorrelationId = context.Saga.CorrelationId,
                                    PlayerId = context.Saga.PlayerId,
                                    ExpDelta = context.Saga.ExpReward,
                                    EnergyDelta = 0 
                                }))
                                .TransitionTo(WaitingForProfile)
                        ),
                    fail => fail
                        .Then(context => context.Saga.FailReason = context.Message.FailReason)
                        .TransitionTo(Failed)
                )
        );

        During(WaitingForInventory,
            When(InventoryGranted)
                .IfElse(context => context.Message.IsSuccess,
                    success => success
                        .Then(context => context.Saga.InventoryDone = true)
                        .PublishAsync(context => context.Init<UpdatePlayerStatsCommand>(new
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            PlayerId = context.Saga.PlayerId,
                            ExpDelta = context.Saga.ExpReward,
                            EnergyDelta = 0 
                        }))
                        .TransitionTo(WaitingForProfile),
                    fail => fail
                        .Then(context => context.Saga.FailReason = context.Message.FailReason)
                        .PublishAsync(context => context.Init<DeductMoneyCommand>(new
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            PlayerId = context.Saga.PlayerId,
                            Amount = context.Saga.MoneyReward,
                            Reason = "Crime Reward Rollback"
                        }))
                        .TransitionTo(Failed)
                )
        );

        During(WaitingForProfile,
            When(PlayerStatsUpdated)
                .IfElse(context => context.Message.IsSuccess,
                    success => success
                        .Then(context => 
                        {
                            context.Saga.ProfileDone = true;
                            context.Saga.CompletedAt = DateTime.UtcNow;
                        })
                        .TransitionTo(Completed),
                    fail => fail
                        .Then(context => context.Saga.FailReason = context.Message.FailReason)
                        .PublishAsync(context => context.Init<DeductMoneyCommand>(new
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            PlayerId = context.Saga.PlayerId,
                            Amount = context.Saga.MoneyReward,
                            Reason = "Crime Reward Rollback"
                        }))
                        .If(context => context.Saga.ItemRewardId.HasValue,
                            binder => binder.PublishAsync(context => context.Init<RevokeItemCommand>(new
                            {
                                CorrelationId = context.Saga.CorrelationId,
                                PlayerId = context.Saga.PlayerId,
                                ItemId = context.Saga.ItemRewardId!.Value,
                                Quantity = 1
                            }))
                        )
                        .TransitionTo(Failed)
                )
        );
    }
}

