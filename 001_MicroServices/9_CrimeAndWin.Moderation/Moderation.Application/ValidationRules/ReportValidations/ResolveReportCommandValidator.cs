using FluentValidation;
using Moderation.Application.Features.Report.Commands;

namespace Moderation.Application.ValidationRules.ReportValidations
{
    public class ResolveReportCommandValidator : AbstractValidator<ResolveReportCommand>
    {
        public ResolveReportCommandValidator()
        {
            RuleFor(x => x.ReportId).NotEmpty();
            RuleFor(x => x.ModeratorId).NotEmpty();
        }
    }
}
