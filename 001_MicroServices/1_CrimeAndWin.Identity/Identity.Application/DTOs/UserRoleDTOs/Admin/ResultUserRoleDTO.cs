namespace Identity.Application.DTOs.UserRoleDTOs.Admin
{
    public sealed class ResultUserRoleDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
