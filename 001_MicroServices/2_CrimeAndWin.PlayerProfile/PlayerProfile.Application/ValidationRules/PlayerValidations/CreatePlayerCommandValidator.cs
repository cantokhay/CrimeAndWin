using FluentValidation;
using PlayerProfile.Application.Features.Player.Commands.CreatePlayer;

namespace PlayerProfile.Application.ValidationRules.PlayerValidations
{
    public sealed class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerCommandValidator()
        {
            RuleFor(x => x.AppUserId).NotEmpty();

            RuleFor(x => x.DisplayName)
                .NotEmpty().MaximumLength(30);

            RuleFor(x => x.AvatarKey)
                .NotEmpty().MaximumLength(64);

            RuleFor(x => x.Power).InclusiveBetween(0, 100_000);
            RuleFor(x => x.Defense).InclusiveBetween(0, 100_000);
            RuleFor(x => x.Agility).InclusiveBetween(0, 100_000);
            RuleFor(x => x.Luck).InclusiveBetween(0, 100_000);

            RuleFor(x => x.EnergyMax).Equal(10_000);
            RuleFor(x => x.EnergyRegenPerMinute).Equal(10);
            RuleFor(x => x.EnergyCurrent).InclusiveBetween(0, 10_000);
        }
    }
}
