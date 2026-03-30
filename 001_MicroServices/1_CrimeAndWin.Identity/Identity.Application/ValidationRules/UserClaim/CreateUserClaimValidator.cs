using FluentValidation;
using Identity.Application.DTOs.UserClaimDTOs.Admin;

namespace Identity.Application.ValidationRules.UserClaim
{
    public class CreateUserClaimValidator : AbstractValidator<CreateUserClaimDTO>
    {
        public CreateUserClaimValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ClaimType).NotEmpty().MinimumLength(2);
            RuleFor(x => x.ClaimValue).NotEmpty().MinimumLength(1);
        }
    }
}
