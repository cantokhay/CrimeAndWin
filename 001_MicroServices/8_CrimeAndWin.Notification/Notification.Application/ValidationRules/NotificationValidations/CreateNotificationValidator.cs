using FluentValidation;
using Notification.Application.Features.Notification.Commands;

namespace Notification.Application.ValidationRules.NotificationValidations
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Message).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Type).NotEmpty();
        }
    }
}
