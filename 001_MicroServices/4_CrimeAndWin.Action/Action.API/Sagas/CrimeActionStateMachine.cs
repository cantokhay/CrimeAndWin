using MassTransit;
using CrimeAndWin.Contracts.Events.Action;
using CrimeAndWin.Contracts.Commands.Economy;
using CrimeAndWin.Contracts.Commands.PlayerProfile;

namespace Action.API.Sagas
{
    public class CrimeActionStateMachine : MassTransitStateMachine<CrimeActionState>
    {
        public State Processing { get; private set; }
        public State DispatchingRewards { get; private set; }
        public State Finalized { get; private set; }

        public Event<CrimeActionStartedEvent> StartCrimeSaga { get; private set; }

        public CrimeActionStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => StartCrimeSaga, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(StartCrimeSaga)
                    .Then(context =>
                    {
                        context.Saga.PlayerId = context.Message.PlayerId;
                        context.Saga.ActionId = context.Message.ActionId;
                        context.Saga.CreatedAt = DateTime.UtcNow;
                        context.Saga.UpdatedAt = DateTime.UtcNow;
                    })
                    .TransitionTo(Processing)
                    .Publish(context => new RewardMoneyCommand
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        PlayerId = context.Saga.PlayerId,
                        Amount = 500,
                        Reason = "Crime Success Reward"
                    })
                    .Publish(context => new IncreasePlayerHeatCommand
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        PlayerId = context.Saga.PlayerId,
                        Amount = 3,
                        Reason = "Law violation detected"
                    })
                    .TransitionTo(DispatchingRewards)
                    .TransitionTo(Finalized) // Placeholder transition for MVP
            );
        }
    }
}
