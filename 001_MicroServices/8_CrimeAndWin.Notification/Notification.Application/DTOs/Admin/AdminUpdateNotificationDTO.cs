namespace Notification.Application.DTOs.Admin
{
    public sealed class AdminUpdateNotificationDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }

        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Type { get; set; } = null!;

    }
}
