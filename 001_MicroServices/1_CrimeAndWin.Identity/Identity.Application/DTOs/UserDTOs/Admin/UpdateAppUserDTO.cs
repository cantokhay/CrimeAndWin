namespace Identity.Application.DTOs.UserDTOs.Admin
{
    public sealed class UpdateAppUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}
