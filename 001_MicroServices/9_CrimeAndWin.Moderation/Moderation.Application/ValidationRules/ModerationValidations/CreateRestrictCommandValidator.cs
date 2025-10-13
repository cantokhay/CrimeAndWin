using FluentValidation;
using Moderation.Application.Features.ModerationAction.Commands.CreateRestriction;

namespace Moderation.Application.ValidationRules.ModerationValidations
{
    public class CreateRestrictCommandValidator : AbstractValidator<CreateRestrictCommand>
    {
        public CreateRestrictCommandValidator()
        {
            RuleFor(x => x.Dto.PlayerId).NotEmpty();
            RuleFor(x => x.Dto.ModeratorId).NotEmpty();
            RuleFor(x => x.Dto.Reason).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Dto.ExpiryDateUtc)
                .Must(d => d is null || d > DateTime.UtcNow)
                .WithMessage("ExpiryDateUtc geçmiş olamaz.");
        }
    }
}
