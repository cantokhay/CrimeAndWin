using FluentValidation;
using Identity.Application.DTOs.RefreshTokenDTOs.Admin;

namespace Identity.Application.ValidationRules.RefreshToken
{
    public class UpdateRefreshTokenValidator : AbstractValidator<UpdateRefreshTokenDTO>
    {
        public UpdateRefreshTokenValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.ExpiresAtUtc).NotEmpty();
        }
    }
}
