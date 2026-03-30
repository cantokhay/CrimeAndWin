using FluentValidation;
using Identity.Application.DTOs.RoleDTOs.Admin;

namespace Identity.Application.ValidationRules.Role
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleDTO>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.NormalizedName).NotEmpty();
        }
    }
}
