using FluentValidation;
using Identity.Application.DTOs.RefreshTokenDTOs.Admin;

namespace Identity.Application.ValidationRules.RefreshToken
{
    public class CreateRefreshTokenValidator : AbstractValidator<CreateRefreshTokenDTO>
    {
        public CreateRefreshTokenValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Token).NotEmpty().MinimumLength(10);
            RuleFor(x => x.ExpiresAtUtc).NotEmpty();
        }
    }
}
