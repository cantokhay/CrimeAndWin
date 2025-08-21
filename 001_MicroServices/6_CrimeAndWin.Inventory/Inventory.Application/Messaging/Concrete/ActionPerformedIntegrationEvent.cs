using Inventory.Application.Messaging.Abstract;

namespace Inventory.Application.Messaging.Concrete
{
    public sealed record ActionPerformedIntegrationEvent
        (
        Guid EventId, 
        DateTime OccurredOnUtc,
        Guid PlayerId, 
        Guid ActionId, 
        IReadOnlyList<ActionRewardItem> Items
        ) 
        : IIntegrationEvent;
}
