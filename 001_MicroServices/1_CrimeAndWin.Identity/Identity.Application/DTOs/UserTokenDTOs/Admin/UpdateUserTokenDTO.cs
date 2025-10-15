namespace Identity.Application.DTOs.UserTokenDTOs.Admin
{
    public sealed class UpdateUserTokenDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
