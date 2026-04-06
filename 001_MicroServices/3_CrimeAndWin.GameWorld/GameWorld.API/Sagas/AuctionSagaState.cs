using MassTransit;

namespace GameWorld.API.Sagas
{
    public class AuctionSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = default!;

        public Guid AuctionId { get; set; }
        public Guid CurrentHighestBidderId { get; set; }
        public decimal CurrentHighestBid { get; set; }
        
        public DateTime LastUpdateAt { get; set; }
    }
}
