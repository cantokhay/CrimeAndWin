using Inventory.Application.Messaging.Abstract;
using MassTransit;

namespace Inventory.Application.Messaging.Concrete
{
    public sealed class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _bus;
        public EventPublisher(IPublishEndpoint bus) => _bus = bus;
        public Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : IIntegrationEvent
            => _bus.Publish(@event, ct);
    }
}
