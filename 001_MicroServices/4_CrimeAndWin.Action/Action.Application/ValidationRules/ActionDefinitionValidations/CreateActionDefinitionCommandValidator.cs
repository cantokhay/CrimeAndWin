using FluentValidation;
using Action.Application.DTOs.ActionDefinitionDTOs;
using Shared.Infrastructure.Validation;

namespace Action.Application.ValidationRules.ActionDefinitionValidations
{
    public class CreateActionDefinitionCommandValidator : AbstractValidator<CreateActionDefinitionDTO>
    {
        public CreateActionDefinitionCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(32)
                .Matches("^[A-Z0-9_]+$").WithMessage("Code yalnizca A-Z, 0-9 ve _ icerebilir.")
                .Must(code => code.Trim() == code).WithMessage("Code basinda/sonunda bosluk olamaz.");

            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.MinPower).GreaterThanOrEqualTo(0);
            RuleFor(x => x.EnergyCost).Positive(); 
            RuleFor(x => x.PowerGain).Positive();
            RuleFor(x => x.MoneyGain).PositiveCurrency();

            // IsActive bool oldugundan NotNull gereksiz
        }
    }
}
