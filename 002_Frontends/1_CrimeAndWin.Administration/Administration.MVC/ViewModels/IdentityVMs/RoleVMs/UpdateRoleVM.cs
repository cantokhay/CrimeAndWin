namespace Administration.MVC.ViewModels.IdentityVMs.RoleVMs
{
    public class UpdateRoleVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
