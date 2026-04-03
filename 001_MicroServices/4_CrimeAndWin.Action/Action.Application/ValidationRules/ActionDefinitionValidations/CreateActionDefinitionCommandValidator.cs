using FluentValidation;
using Action.Application.DTOs.ActionDefinitionDTOs;

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
            RuleFor(x => x.EnergyCost).GreaterThan(0); // Fixed: replaced .Positive() with .GreaterThan(0)
            RuleFor(x => x.PowerGain).GreaterThan(0);
            RuleFor(x => x.MoneyGain)
                .GreaterThan(0).WithMessage("MoneyGain must be a positive value.")
                .WithMessage("MoneyGain must have at most 2 decimal places.");

            // IsActive bool oldugundan NotNull gereksiz
        }
    }
}
