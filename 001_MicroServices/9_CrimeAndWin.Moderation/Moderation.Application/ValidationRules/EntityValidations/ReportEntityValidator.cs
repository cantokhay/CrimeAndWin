using Moderation.Domain.Entities;
using Moderation.Domain.VOs;
using FluentValidation;
using Shared.Infrastructure.Validation;

namespace Moderation.Application.ValidationRules.EntityValidations;

public class ReportEntityValidator : BaseEntityValidator<Report>
{
    public ReportEntityValidator()
    {
        RuleFor(x => x.ReporterId).NotEmpty();
        RuleFor(x => x.ReportedPlayerId).NotEmpty().NotEqual(x => x.ReporterId).WithMessage("Bir oyuncu kendisini şikayet edemez.");
        RuleFor(x => x.Reason).NotNull().SetValidator(new ReportReasonValidator());
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.ResolvedAtUtc)
            .Must(date => date == null || date.Value < DateTime.UtcNow)
            .WithMessage("Çözülme tarihi geçmişte olmalıdır.");
    }
}

public class ReportReasonValidator : AbstractValidator<ReportReason>
{
    public ReportReasonValidator()
    {
        RuleFor(x => x.Value).NotEmpty().MaximumLength(50);
    }
}

public class ModerationActionEntityValidator : BaseEntityValidator<ModerationAction>
{
    public ModerationActionEntityValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.ModeratorId).NotEmpty();
        RuleFor(x => x.ActionType).NotEmpty().MaximumLength(50).MatchesRegex("^[A-Z_]+$", "İşlem Tipi");
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(500);
        RuleFor(x => x.ActionDateUtc).IsPast();
        RuleFor(x => x.ExpiryDateUtc)
            .Must((entity, expiry) => !expiry.HasValue || expiry > entity.ActionDateUtc)
            .WithMessage("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");
    }
}
