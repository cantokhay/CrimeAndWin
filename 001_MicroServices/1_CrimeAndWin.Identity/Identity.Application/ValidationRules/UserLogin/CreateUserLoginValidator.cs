using FluentValidation;
using Identity.Application.DTOs.UserLoginDTOs.Admin;

namespace Identity.Application.ValidationRules.UserLogin
{
    public class CreateUserLoginValidator : AbstractValidator<CreateUserLoginDTO>
    {
        public CreateUserLoginValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.LoginProvider).NotEmpty().MinimumLength(2);
            RuleFor(x => x.ProviderKey).NotEmpty();
        }
    }
}
