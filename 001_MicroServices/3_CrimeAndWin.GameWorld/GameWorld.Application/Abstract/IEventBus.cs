namespace GameWorld.Application.Abstract
{
    public interface IEventBus
    {
        Task PublishAsync<T>(string topic, T @event, CancellationToken ct = default);
    }
}
