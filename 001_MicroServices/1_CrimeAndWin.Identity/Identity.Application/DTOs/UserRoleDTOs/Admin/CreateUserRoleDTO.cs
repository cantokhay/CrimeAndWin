namespace Identity.Application.DTOs.UserRoleDTOs.Admin
{
    public sealed class CreateUserRoleDTO
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
