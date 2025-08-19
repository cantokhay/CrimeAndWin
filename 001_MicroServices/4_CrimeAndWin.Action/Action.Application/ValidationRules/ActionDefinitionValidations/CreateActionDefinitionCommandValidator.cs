using Action.Application.DTOs;
using FluentValidation;

namespace Action.Application.ValidationRules.ActionDefinitionValidations
{
    public class CreateActionDefinitionCommandValidator : AbstractValidator<CreateActionDefinitionDTO>
    {
        public CreateActionDefinitionCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(32)
                .Matches("^[A-Z0-9_]+$").WithMessage("Code yalnızca A-Z, 0-9 ve _ içerebilir.")
                .Must(code => code.Trim() == code).WithMessage("Code başında/sonunda boşluk olamaz.");

            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.MinPower).GreaterThanOrEqualTo(0);
            RuleFor(x => x.EnergyCost).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PowerGain).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MoneyGain).GreaterThanOrEqualTo(0m);

            // IsActive bool olduğundan NotNull gereksiz
        }
    }
}
