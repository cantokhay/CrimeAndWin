namespace Identity.Application.DTOs.UserLoginDTOs.Admin
{
    public sealed class CreateUserLoginDTO
    {
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
    }
}
