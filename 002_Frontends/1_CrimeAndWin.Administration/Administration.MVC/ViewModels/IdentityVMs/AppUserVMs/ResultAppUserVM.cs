namespace Administration.MVC.ViewModels.IdentityVMs.AppUserVMs
{
    public class ResultAppUserVM
    {
        public Guid Id { get; set; }

        // BaseEntity
        public DateTime CreatedAtUtc { get; set; }
        public bool IsDeleted { get; set; }

        // Identity
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }

        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
