using MassTransit;
using CrimeAndWin.Contracts.Events.Action;
using CrimeAndWin.Contracts.Commands.Economy;
using CrimeAndWin.Contracts.Commands.PlayerProfile;

namespace Action.API.Sagas
{
    public class RaidStateMachine : MassTransitStateMachine<RaidSagaState>
    {
        public State Punishing { get; private set; }
        public State Completed { get; private set; }

        public Event<RaidStartedEvent> RaidStarted { get; private set; }

        public RaidStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => RaidStarted, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(RaidStarted)
                    .Then(context =>
                    {
                        context.Saga.PlayerId = context.Message.PlayerId;
                        context.Saga.CreatedAt = DateTime.UtcNow;
                    })
                    .TransitionTo(Punishing)
                    .Publish(context => new SeizeBlackMoneyCommand
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        PlayerId = context.Saga.PlayerId
                    })
                    .Publish(context => new StartPrisonSentenceCommand
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        PlayerId = context.Saga.PlayerId,
                        DurationMinutes = 15
                    })
                    .TransitionTo(Completed)
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}
