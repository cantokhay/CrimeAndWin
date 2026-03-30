namespace Administration.MVC.ViewModels.IdentityVMs.UserRoleVMs
{
    public class ResultUserRoleVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
    }
}
