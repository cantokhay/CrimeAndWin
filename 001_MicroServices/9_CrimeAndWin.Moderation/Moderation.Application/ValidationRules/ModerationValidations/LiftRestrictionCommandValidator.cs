using FluentValidation;
using Moderation.Application.Features.ModerationAction.Commands;

namespace Moderation.Application.ValidationRules.ModerationValidations
{
    public class LiftRestrictionCommandValidator : AbstractValidator<LiftRestrictionCommand>
    {
        public LiftRestrictionCommandValidator()
        {
            RuleFor(x => x.Dto.PlayerId).NotEmpty();
            RuleFor(x => x.Dto.ModeratorId).NotEmpty();
            RuleFor(x => x.Dto.Reason).NotEmpty().MaximumLength(500);
        }
    }
}
