using FluentValidation;
using PlayerProfile.Application.Features.Player.Commands.UpdateAvatar;

namespace PlayerProfile.Application.ValidationRules.PlayerValidations
{

    public sealed class UpdateAvatarCommandValidator : AbstractValidator<UpdateAvatarCommand>
    {
        public UpdateAvatarCommandValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.AvatarKey).NotEmpty().MaximumLength(64);
        }
    }
}
