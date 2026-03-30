namespace Identity.Application.DTOs.UserClaimDTOs.Admin
{
    public sealed class UpdateUserClaimDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ClaimType { get; set; } = null!;
        public string ClaimValue { get; set; } = null!;
    }
}
