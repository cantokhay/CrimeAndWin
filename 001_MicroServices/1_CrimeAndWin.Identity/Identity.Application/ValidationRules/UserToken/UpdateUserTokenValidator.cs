using FluentValidation;
using Identity.Application.DTOs.UserTokenDTOs.Admin;

namespace Identity.Application.ValidationRules.UserToken
{
    public class UpdateUserTokenValidator : AbstractValidator<UpdateUserTokenDTO>
    {
        public UpdateUserTokenValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.LoginProvider).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
