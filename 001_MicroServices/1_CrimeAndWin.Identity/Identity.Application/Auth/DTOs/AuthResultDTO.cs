namespace Identity.Application.Auth.DTOs
{
    public sealed class AuthResultDTO
    {
        public string AccessToken { get; set; } = null!;
        public DateTime ExpiresAtUtc { get; set; }
        public string RefreshToken { get; set; } = null!; // şimdilik üretiyoruz; ileride refresh akışı ekleriz
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
