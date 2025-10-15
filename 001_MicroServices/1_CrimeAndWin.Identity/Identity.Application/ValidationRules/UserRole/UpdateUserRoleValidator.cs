using FluentValidation;
using Identity.Application.DTOs.UserRoleDTOs.Admin;

namespace Identity.Application.ValidationRules.UserRole
{
    public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleDTO>
    {
        public UpdateUserRoleValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.RoleId).NotEmpty();
        }
    }
}
