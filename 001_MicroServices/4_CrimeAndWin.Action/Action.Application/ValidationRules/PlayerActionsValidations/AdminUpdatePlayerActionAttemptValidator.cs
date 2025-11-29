using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using FluentValidation;

namespace Action.Application.ValidationRules.PlayerActionsValidations
{
    public sealed class AdminUpdatePlayerActionAttemptValidator : AbstractValidator<AdminUpdatePlayerActionAttemptDTO>
    {
        public AdminUpdatePlayerActionAttemptValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.ActionDefinitionId).NotEmpty();
            RuleFor(x => x.SuccessRate).InclusiveBetween(0.0, 1.0);
        }
    }
}
