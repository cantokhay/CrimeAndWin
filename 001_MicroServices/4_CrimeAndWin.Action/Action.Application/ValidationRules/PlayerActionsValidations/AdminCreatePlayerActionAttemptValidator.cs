using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using FluentValidation;

namespace Action.Application.ValidationRules.PlayerActionsValidations
{
    public sealed class AdminCreatePlayerActionAttemptValidator : AbstractValidator<AdminCreatePlayerActionAttemptDTO>
    {
        public AdminCreatePlayerActionAttemptValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.ActionDefinitionId).NotEmpty();
            RuleFor(x => x.SuccessRate).InclusiveBetween(0.0, 1.0);
        }
    }
}
