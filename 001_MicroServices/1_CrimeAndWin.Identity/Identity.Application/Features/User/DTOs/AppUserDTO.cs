namespace Identity.Application.Features.User.DTOs
{
    public sealed class AppUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
    }
}
