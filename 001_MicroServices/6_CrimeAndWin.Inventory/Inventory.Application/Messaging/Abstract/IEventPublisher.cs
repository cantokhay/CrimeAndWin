namespace Inventory.Application.Messaging.Abstract
{
    public interface IEventPublisher 
    { 
        Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : IIntegrationEvent; 
    }

}
