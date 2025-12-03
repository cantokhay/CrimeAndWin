using FluentValidation;
using Moderation.Application.DTOs.ReportDTOs.Admin;

namespace Moderation.Application.ValidationRules.ReportValidations
{
    public sealed class AdminUpdateReportValidator : AbstractValidator<AdminUpdateReportDTO>
    {
        public AdminUpdateReportValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ReporterId).NotEmpty();
            RuleFor(x => x.ReportedPlayerId).NotEmpty();
            RuleFor(x => x.Reason).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        }
    }
}
