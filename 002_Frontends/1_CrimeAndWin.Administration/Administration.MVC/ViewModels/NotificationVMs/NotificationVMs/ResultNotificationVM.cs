namespace Administration.MVC.ViewModels.NotificationVMs.NotificationVMs
{
    public class ResultNotificationVM
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }

        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Type { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }

        public string? PlayerDisplay { get; set; }
    }
}
