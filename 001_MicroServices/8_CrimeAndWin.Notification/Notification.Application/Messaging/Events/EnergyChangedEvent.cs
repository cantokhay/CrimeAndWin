namespace Notification.Application.Messaging.Events
{
    public record EnergyChangedEvent
        (
        Guid PlayerId, 
        int Current, 
        int Max, 
        DateTime OccurredAtUtc
        );
}
