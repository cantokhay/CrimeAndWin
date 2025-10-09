using MassTransit;
using Moderation.Application.Messaging.Abstract;

namespace Moderation.Application.Messaging.Concrete
{
    public class MassTransitEventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publish;
        public MassTransitEventPublisher(IPublishEndpoint publish) => _publish = publish;
        public Task PublishAsync<T>(T @event, string topic = null) => _publish.Publish(@event);
    }
}
