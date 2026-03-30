using FluentValidation;
using Identity.Application.DTOs.UserDTOs.Admin;

namespace Identity.Application.ValidationRules.AppUser
{
    public class CreateAppUserValidator : AbstractValidator<CreateAppUserDTO>
    {
        public CreateAppUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PasswordHash).NotEmpty();
        }
    }
}
