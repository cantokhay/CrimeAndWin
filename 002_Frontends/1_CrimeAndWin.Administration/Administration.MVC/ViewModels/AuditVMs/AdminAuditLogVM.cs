namespace Administration.MVC.ViewModels.AuditVMs
{
    public class AdminAuditLogVM
    {
        public Guid Id { get; set; }
        public string AdminUser { get; set; }
        public string ActionType { get; set; } // "CREATE", "UPDATE", "DELETE", "SYSTEM"
        public string EntityName { get; set; } // "User", "Player", "GlobalSettings"
        public string EntityId { get; set; }
        public string Description { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string IpAddress { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsSuccess { get; set; }
    }
}
