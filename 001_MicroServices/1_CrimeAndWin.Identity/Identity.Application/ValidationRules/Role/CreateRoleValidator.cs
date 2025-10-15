using FluentValidation;
using Identity.Application.DTOs.RoleDTOs.Admin;

namespace Identity.Application.ValidationRules.Role
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDTO>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.NormalizedName).NotEmpty().MinimumLength(3);
        }
    }
}
