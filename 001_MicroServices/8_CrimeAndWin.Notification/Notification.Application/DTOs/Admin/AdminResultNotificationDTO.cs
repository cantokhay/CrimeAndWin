namespace Notification.Application.DTOs.Admin
{
    public sealed class AdminResultNotificationDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }

        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Type { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
