namespace Action.Application.Abstract
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event, CancellationToken ct = default);
    }
}
