using MassTransit;
using CrimeAndWin.Contracts.Events.Economy;

namespace Economy.API.Sagas
{
    public class LaunderingStateMachine : MassTransitStateMachine<LaunderingSagaState>
    {
        public State Converting { get; private set; }
        public State Finished { get; private set; }

        public Event<LaunderingStartedEvent> StartLaunderingSaga { get; private set; }

        public LaunderingStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => StartLaunderingSaga, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(StartLaunderingSaga)
                    .Then(context =>
                    {
                        context.Saga.PlayerId = context.Message.PlayerId;
                        context.Saga.InputBlackAmount = context.Message.AmountToLaunder;
                        context.Saga.Efficiency = context.Message.EfficiencyRate;
                        context.Saga.CreatedAt = DateTime.UtcNow;
                        context.Saga.FinalCashAmount = context.Saga.InputBlackAmount * context.Saga.Efficiency;
                    })
                    .TransitionTo(Converting)
                    .Publish(context => new LaunderingCompletedEvent
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        PlayerId = context.Saga.PlayerId,
                        FinalCleanAmount = context.Saga.FinalCashAmount,
                        BurnedAmount = context.Saga.InputBlackAmount - context.Saga.FinalCashAmount
                    })
                    .TransitionTo(Finished)
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}
