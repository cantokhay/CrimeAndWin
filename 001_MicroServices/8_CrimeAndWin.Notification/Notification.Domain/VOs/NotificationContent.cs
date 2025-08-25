namespace Notification.Domain.VOs
{
    public record NotificationContent
        (
        string Title, 
        string Message, 
        string Type
        );
}
