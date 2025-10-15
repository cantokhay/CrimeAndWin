using FluentValidation;
using Identity.Application.DTOs.UserClaimDTOs.Admin;

namespace Identity.Application.ValidationRules.UserClaim
{
    public class UpdateUserClaimValidator : AbstractValidator<UpdateUserClaimDTO>
    {
        public UpdateUserClaimValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ClaimType).NotEmpty();
            RuleFor(x => x.ClaimValue).NotEmpty();
        }
    }
}
