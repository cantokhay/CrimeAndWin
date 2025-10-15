namespace Identity.Application.DTOs.RefreshTokenDTOs.Admin
{
    public sealed class CreateRefreshTokenDTO
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? RevokedAtUtc { get; set; }
        public string? ReplacedByToken { get; set; }
    }
}
