using Action.Application.DTOs.ActionAttemptDTOs;
using FluentValidation;

namespace Action.Application.ValidationRules.PlayerActionValidations
{
    public class PerformPlayerActionAttemptCommandValidator : AbstractValidator<PlayerActionAttemptDTO>
    {
        public PerformPlayerActionAttemptCommandValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.ActionDefinitionId).NotEmpty();
            RuleFor(x => x.SuccessRate).InclusiveBetween(0d, 1d);
        }
    }
}
