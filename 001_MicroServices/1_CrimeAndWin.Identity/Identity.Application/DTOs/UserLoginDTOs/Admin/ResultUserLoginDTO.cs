namespace Identity.Application.DTOs.UserLoginDTOs.Admin
{
    public sealed class ResultUserLoginDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
