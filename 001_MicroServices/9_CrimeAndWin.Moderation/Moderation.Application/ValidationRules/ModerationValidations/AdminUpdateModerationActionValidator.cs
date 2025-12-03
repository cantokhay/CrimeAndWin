using FluentValidation;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;

namespace Moderation.Application.ValidationRules.ModerationValidations
{
    public sealed class AdminUpdateModerationActionValidator : AbstractValidator<AdminUpdateModerationActionDTO>
    {
        public AdminUpdateModerationActionValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.ModeratorId).NotEmpty();
            RuleFor(x => x.ActionType).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Reason).NotEmpty().MaximumLength(500);

            RuleFor(x => x.ExpiryDateUtc)
                .Must(d => d is null || d > DateTime.UtcNow)
                .WithMessage("ExpiryDateUtc geçmiş olamaz.");
        }
    }
}
