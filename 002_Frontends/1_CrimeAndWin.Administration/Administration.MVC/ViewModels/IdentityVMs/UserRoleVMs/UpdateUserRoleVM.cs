using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.IdentityVMs.UserRoleVMs
{
    public class UpdateUserRoleVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public List<SelectListItem> UserOptions { get; set; } = new();
        public List<SelectListItem> RoleOptions { get; set; } = new();
    }
}
