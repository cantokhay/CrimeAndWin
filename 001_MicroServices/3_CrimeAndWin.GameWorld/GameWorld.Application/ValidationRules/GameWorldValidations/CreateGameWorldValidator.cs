using FluentValidation;
using GameWorld.Application.Features.GameWorld.Commands.CreateGameWorld;

namespace GameWorld.Application.ValidationRules.GameWorldValidations
{
    public class CreateGameWorldValidator : AbstractValidator<CreateGameWorldCommand>
    {
        public CreateGameWorldValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim boş olamaz")
                .MaximumLength(100).WithMessage("İsim en fazla 100 karakter");

            RuleFor(x => x.MaxEnergy)
                .GreaterThan(0).WithMessage("MaxEnergy > 0 olmalı")
                .LessThanOrEqualTo(1000);

            RuleFor(x => x.RegenRatePerHour)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(1000);
        }
    }
}
