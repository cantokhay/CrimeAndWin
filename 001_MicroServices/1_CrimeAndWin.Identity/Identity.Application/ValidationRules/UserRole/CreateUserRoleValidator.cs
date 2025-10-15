using FluentValidation;
using Identity.Application.DTOs.UserRoleDTOs.Admin;

namespace Identity.Application.ValidationRules.UserRole
{
    public class CreateUserRoleValidator : AbstractValidator<CreateUserRoleDTO>
    {
        public CreateUserRoleValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.RoleId).NotEmpty();
        }
    }
}
