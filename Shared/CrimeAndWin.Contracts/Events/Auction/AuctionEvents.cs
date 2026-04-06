namespace CrimeAndWin.Contracts.Events.Auction
{
    public record BidPlacedEvent
    {
        public Guid CorrelationId { get; init; }
        public Guid AuctionId { get; init; }
        public Guid PlayerId { get; init; }
        public decimal Amount { get; init; }
    }

    public record AuctionEndedEvent
    {
        public Guid CorrelationId { get; init; }
        public Guid AuctionId { get; init; }
        public Guid WinnerId { get; init; }
        public decimal WinningBid { get; init; }
    }
}

namespace CrimeAndWin.Contracts.Commands.Economy
{
    public record LockBidAmountCommand
    {
        public Guid CorrelationId { get; init; }
        public Guid PlayerId { get; init; }
        public decimal Amount { get; init; }
    }

    public record RefundBidAmountCommand
    {
        public Guid CorrelationId { get; init; }
        public Guid PlayerId { get; init; }
        public decimal Amount { get; init; }
    }
}
