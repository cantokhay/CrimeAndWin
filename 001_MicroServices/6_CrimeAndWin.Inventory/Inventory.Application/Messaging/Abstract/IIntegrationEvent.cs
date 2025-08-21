namespace Inventory.Application.Messaging.Abstract
{
    public interface IIntegrationEvent 
    { 
        Guid EventId { get; } 
        DateTime OccurredOnUtc { get; } 
    }
}
