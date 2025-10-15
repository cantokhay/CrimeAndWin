namespace Identity.Application.DTOs.UserDTOs.Admin
{
    public sealed class CreateAppUserDTO
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}
