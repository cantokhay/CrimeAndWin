using MassTransit;
using CrimeAndWin.Contracts.Events.Auction;
using CrimeAndWin.Contracts.Commands.Economy;

namespace GameWorld.API.Sagas
{
    public class AuctionStateMachine : MassTransitStateMachine<AuctionSagaState>
    {
        public State Active { get; private set; }
        public State Finished { get; private set; }

        public Event<BidPlacedEvent> BidPlaced { get; private set; }

        public AuctionStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => BidPlaced, x => x.CorrelateBy((instance, context) => 
                instance.AuctionId == context.Message.AuctionId)
                .SelectId(context => context.Message.CorrelationId));

            Initially(
                When(BidPlaced)
                    .Then(context =>
                    {
                        context.Saga.AuctionId = context.Message.AuctionId;
                        context.Saga.CurrentHighestBidderId = context.Message.PlayerId;
                        context.Saga.CurrentHighestBid = context.Message.Amount;
                        context.Saga.LastUpdateAt = DateTime.UtcNow;
                    })
                    .TransitionTo(Active)
                    .Publish(context => new LockBidAmountCommand
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        PlayerId = context.Saga.CurrentHighestBidderId,
                        Amount = context.Saga.CurrentHighestBid
                    })
            );

            During(Active,
                When(BidPlaced)
                    .If(context => context.Message.Amount > context.Saga.CurrentHighestBid,
                        binder => binder
                            .Publish(context => new RefundBidAmountCommand // Refund old bidder
                            {
                                CorrelationId = context.Saga.CorrelationId,
                                PlayerId = context.Saga.CurrentHighestBidderId,
                                Amount = context.Saga.CurrentHighestBid
                            })
                            .Then(context =>
                            {
                                context.Saga.CurrentHighestBidderId = context.Message.PlayerId;
                                context.Saga.CurrentHighestBid = context.Message.Amount;
                                context.Saga.LastUpdateAt = DateTime.UtcNow;
                            })
                            .Publish(context => new LockBidAmountCommand // Lock new bidder
                            {
                                CorrelationId = context.Saga.CorrelationId,
                                PlayerId = context.Saga.CurrentHighestBidderId,
                                Amount = context.Saga.CurrentHighestBid
                            })
                    )
            );
        }
    }
}
