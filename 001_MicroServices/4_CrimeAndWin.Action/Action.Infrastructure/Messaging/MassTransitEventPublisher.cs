﻿using Action.Application.Abstract;
using MassTransit;

namespace Action.Infrastructure.Messaging
{
    public sealed class MassTransitEventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publish;
        public MassTransitEventPublisher(IPublishEndpoint publish) => _publish = publish;
        public Task PublishAsync<T>(T @event, CancellationToken ct = default) => _publish.Publish(@event, ct);
    }
}
