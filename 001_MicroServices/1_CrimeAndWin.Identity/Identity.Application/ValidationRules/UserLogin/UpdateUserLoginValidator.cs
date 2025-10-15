using FluentValidation;
using Identity.Application.DTOs.UserLoginDTOs.Admin;

namespace Identity.Application.ValidationRules.UserLogin
{
    public class UpdateUserLoginValidator : AbstractValidator<UpdateUserLoginDTO>
    {
        public UpdateUserLoginValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.LoginProvider).NotEmpty();
            RuleFor(x => x.ProviderKey).NotEmpty();
        }
    }
}
