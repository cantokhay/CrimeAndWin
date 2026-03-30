namespace Identity.Application.DTOs.UserLoginDTOs.Admin
{
    public sealed class UpdateUserLoginDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
    }
}
