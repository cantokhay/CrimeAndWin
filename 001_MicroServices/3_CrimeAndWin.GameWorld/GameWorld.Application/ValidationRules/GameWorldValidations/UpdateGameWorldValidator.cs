using FluentValidation;
using GameWorld.Application.Features.GameWorld.Commands.UpdateGameWorld;

namespace GameWorld.Application.ValidationRules.GameWorldValidations
{
    public class UpdateGameWorldValidator : AbstractValidator<UpdateGameWorldCommand>
    {
        public UpdateGameWorldValidator()
        {
            RuleFor(x => x.GameWorldId).NotEmpty();
            RuleFor(x => x.MaxEnergy).GreaterThan(0).LessThanOrEqualTo(1000);
            RuleFor(x => x.RegenRatePerHour).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1000);
        }
    }
}
