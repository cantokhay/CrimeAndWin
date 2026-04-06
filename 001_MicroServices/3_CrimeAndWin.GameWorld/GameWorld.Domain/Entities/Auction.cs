using Shared.Domain;

namespace GameWorld.Domain.Entities
{
    public sealed class Auction : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        
        // Pricing
        public decimal BasePrice { get; set; }
        public decimal CurrentBid { get; set; }
        public decimal MinBidIncrement { get; set; } = 1000;
        
        // Bidders
        public Guid? HighestBidderId { get; set; }
        public string? HighestBidderName { get; set; }
        
        // Schedule
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        
        // Status
        public bool IsFinished { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
