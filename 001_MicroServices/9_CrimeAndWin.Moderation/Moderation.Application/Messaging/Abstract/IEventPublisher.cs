namespace Moderation.Application.Messaging.Abstract
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event, string topic = null);
    }
}
