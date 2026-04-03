using MassTransit;
using CrimeAndWin.Saga.States;
using CrimeAndWin.Contracts.Events.Leadership;
using CrimeAndWin.Contracts.Events.PlayerProfile;
using CrimeAndWin.Contracts.Commands.PlayerProfile;

namespace CrimeAndWin.Saga.StateMachines;

public class RankUpdateStateMachine : MassTransitStateMachine<RankUpdateState>
{
    public State Processing { get; private set; } = null!;
    public State Completed { get; private set; } = null!;
    public State Failed { get; private set; } = null!;

    public Event<RankChangedEvent> RankChanged { get; private set; } = null!;
    public Event<PlayerStatsUpdatedEvent> PlayerStatsUpdated { get; private set; } = null!;

    public RankUpdateStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => RankChanged, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => PlayerStatsUpdated, x => x.CorrelateById(m => m.Message.CorrelationId));

        Initially(
            When(RankChanged)
                .Then(context =>
                {
                    context.Saga.PlayerId = context.Message.PlayerId;
                    context.Saga.NewRank = context.Message.NewRank;
                    context.Saga.OldRank = context.Message.OldRank;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .PublishAsync(context => context.Init<UpdatePlayerStatsCommand>(new
                {
                    CorrelationId = context.Saga.CorrelationId,
                    PlayerId = context.Saga.PlayerId,
                    ExpDelta = 0,
                    EnergyDelta = 0
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


