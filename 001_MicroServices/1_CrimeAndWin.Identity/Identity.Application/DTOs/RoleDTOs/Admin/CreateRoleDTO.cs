namespace Identity.Application.DTOs.RoleDTOs.Admin
{
    public sealed class CreateRoleDTO
    {
        public string Name { get; set; } = null!;
        public string NormalizedName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
