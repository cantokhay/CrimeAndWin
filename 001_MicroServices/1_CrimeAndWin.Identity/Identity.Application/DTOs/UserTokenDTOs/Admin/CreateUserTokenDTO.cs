namespace Identity.Application.DTOs.UserTokenDTOs.Admin
{
    public sealed class CreateUserTokenDTO
    {
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
