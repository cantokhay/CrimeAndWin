namespace Identity.Application.DTOs.RoleDTOs.Admin
{
    public sealed class ResultRoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string NormalizedName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
