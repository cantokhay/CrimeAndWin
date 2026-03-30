namespace Notification.Application.DTOs
{
    public class CreateNotificationDTO
    {
        public Guid PlayerId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
