using Notification.Domain.VOs;
using FluentValidation;
using Shared.Infrastructure.Validation;

namespace Notification.Application.ValidationRules.EntityValidations;

public class NotificationEntityValidator : BaseEntityValidator<Domain.Entities.Notification>
{
    public NotificationEntityValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.Content).NotNull().SetValidator(new NotificationContentValidator());
    }
}

public class NotificationContentValidator : AbstractValidator<NotificationContent>
{
    public NotificationContentValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Message).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Type).NotEmpty().MaximumLength(50).MatchesRegex("^[A-Z_]+$", "Bildirim Tipi");
    }
}

