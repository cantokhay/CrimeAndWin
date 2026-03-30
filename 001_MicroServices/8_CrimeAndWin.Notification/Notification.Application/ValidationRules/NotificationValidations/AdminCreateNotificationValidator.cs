using FluentValidation;
using Notification.Application.DTOs.Admin;

namespace Notification.Application.ValidationRules.NotificationValidations
{
    public sealed class AdminCreateNotificationValidator : AbstractValidator<AdminCreateNotificationDTO>
    {
        public AdminCreateNotificationValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.Type).NotEmpty().MaximumLength(64);
        }
    }
}
