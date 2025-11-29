using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using FluentValidation;

namespace Action.Application.ValidationRules.ActionDefinitionValidations
{
    public sealed class AdminUpdateActionDefinitionValidator : AbstractValidator<AdminUpdateActionDefinitionDTO>
    {
        public AdminUpdateActionDefinitionValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Code).NotEmpty().MaximumLength(32);
            RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(256);
            RuleFor(x => x.MinPower).GreaterThanOrEqualTo(0);
            RuleFor(x => x.EnergyCost).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PowerGain).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MoneyGain).GreaterThanOrEqualTo(0);
        }
    }
}
