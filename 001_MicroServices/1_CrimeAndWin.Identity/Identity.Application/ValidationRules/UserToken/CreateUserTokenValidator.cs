using FluentValidation;
using Identity.Application.DTOs.UserTokenDTOs.Admin;

namespace Identity.Application.ValidationRules.UserToken
{
    public class CreateUserTokenValidator : AbstractValidator<CreateUserTokenDTO>
    {
        public CreateUserTokenValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.LoginProvider).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        }
    }
}
