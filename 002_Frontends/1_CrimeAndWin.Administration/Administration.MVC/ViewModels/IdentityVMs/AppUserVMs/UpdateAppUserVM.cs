namespace Administration.MVC.ViewModels.IdentityVMs.AppUserVMs
{
    public class UpdateAppUserVM
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public int AccessFailedCount { get; set; }
    }
}
