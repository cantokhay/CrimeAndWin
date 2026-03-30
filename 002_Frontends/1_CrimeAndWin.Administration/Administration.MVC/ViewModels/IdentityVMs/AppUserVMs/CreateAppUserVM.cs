namespace Administration.MVC.ViewModels.IdentityVMs.AppUserVMs
{
    public class CreateAppUserVM
    {
        public string UserName { get; set; } = null!; 
        public string Email { get; set; } = null!; 
        public string PasswordHash { get; set; } = null!; 
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
}
