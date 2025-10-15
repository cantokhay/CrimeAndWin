namespace Identity.Application.DTOs.UserClaimDTOs.Admin
{
    public sealed class CreateUserClaimDTO
    {
        public Guid UserId { get; set; }
        public string ClaimType { get; set; } = null!;
        public string ClaimValue { get; set; } = null!;
    }
}
