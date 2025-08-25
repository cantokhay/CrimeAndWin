namespace Notification.Application.DTOs
{
    public class GetByIdNotificationDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
