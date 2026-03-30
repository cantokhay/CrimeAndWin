namespace Identity.Application.DTOs.UserRoleDTOs.Admin
{
    public sealed class UpdateUserRoleDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
