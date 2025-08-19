using GameWorld.Application.Abstract;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GameWorld.Infrastructure.Messaging
{
    public class EventBusStub : IEventBus
    {
        private readonly ILogger<EventBusStub> _logger;
        public EventBusStub(ILogger<EventBusStub> logger) => _logger = logger;

        public Task PublishAsync<T>(string topic, T @event, CancellationToken ct = default)
        {
            _logger.LogInformation("Published to {Topic}: {Payload}", topic, JsonSerializer.Serialize(@event));
            return Task.CompletedTask;
        }
    }
}
