using MassTransit;
using CrimeAndWin.Saga.States;
using CrimeAndWin.Contracts.Events.Action;
using CrimeAndWin.Contracts.Events.PlayerProfile;
using CrimeAndWin.Contracts.Commands.PlayerProfile;

namespace CrimeAndWin.Saga.StateMachines;

public class CrimeActionStateMachine : MassTransitStateMachine<CrimeActionState>
{
    public State Processing { get; private set; } = null!;
    public State Completed { get; private set; } = null!;
    public State Failed { get; private set; } = null!;

    public Event<CrimeCompletedEvent> CrimeCompleted { get; private set; } = null!;
    public Event<PlayerStatsUpdatedEvent> PlayerStatsUpdated { get; private set; } = null!;

    public CrimeActionStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CrimeCompleted, x => 
        {
            x.CorrelateBy(state => state.ActionId, context => context.Message.ActionId);
            x.SelectId(context => NewId.NextGuid());
        });
        
        Event(() => PlayerStatsUpdated, x => x.CorrelateById(m => m.Message.CorrelationId));

        Initially(
            When(CrimeCompleted, context => context.Message.IsSuccess)
                .Then(context =>
                {
                    context.Saga.PlayerId = context.Message.PlayerId;
                    context.Saga.ActionId = context.Message.ActionId;
                    context.Saga.ExpDelta = context.Message.ExpReward;
                    context.Saga.EnergyDelta = -context.Message.EnergyCost;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .PublishAsync(context => context.Init<UpdatePlayerStatsCommand>(new
                {
                    CorrelationId = context.Saga.CorrelationId,
                    PlayerId = context.Saga.PlayerId,
                    ExpDelta = context.Saga.ExpDelta,
                    EnergyDelta = context.Saga.EnergyDelta
                }))
                .TransitionTo(Processing)
        );

        During(Processing,
            When(PlayerStatsUpdated)
                .IfElse(context => context.Message.IsSuccess,
                    success => success.TransitionTo(Completed),
                    fail => fail
                        .Then(context => context.Saga.FailReason = context.Message.FailReason)
                        .TransitionTo(Failed)
                )
        );
    }
}
