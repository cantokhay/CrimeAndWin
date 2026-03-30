using FluentValidation;
using Identity.Application.DTOs.UserDTOs.Admin;

namespace Identity.Application.ValidationRules.AppUser
{
    public class UpdateAppUserValidator : AbstractValidator<UpdateAppUserDTO>
    {
        public UpdateAppUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
