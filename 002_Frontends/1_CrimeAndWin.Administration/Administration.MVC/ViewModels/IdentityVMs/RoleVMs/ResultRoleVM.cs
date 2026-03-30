namespace Administration.MVC.ViewModels.IdentityVMs.RoleVMs
{
    public class ResultRoleVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
