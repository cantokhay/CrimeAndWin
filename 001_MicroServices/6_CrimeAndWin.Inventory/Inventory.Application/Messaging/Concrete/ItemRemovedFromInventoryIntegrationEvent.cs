using Inventory.Application.Messaging.Abstract;

namespace Inventory.Application.Messaging.Concrete
{
    public sealed record ItemRemovedFromInventoryIntegrationEvent
        (
        Guid EventId, 
        DateTime OccurredOnUtc,
        Guid PlayerId, 
        Guid InventoryId, 
        Guid ItemId, 
        int Quantity
        ) : IIntegrationEvent;
}
