using Inventory.Application.Messaging.Abstract;

namespace Inventory.Application.Messaging.Concrete
{
    public sealed record ItemAddedToInventoryIntegrationEvent
        (
        Guid EventId, 
        DateTime OccurredOnUtc,
        Guid PlayerId, 
        Guid InventoryId, 
        Guid ItemId, 
        string Name, 
        int Quantity
        ) : IIntegrationEvent;
}
