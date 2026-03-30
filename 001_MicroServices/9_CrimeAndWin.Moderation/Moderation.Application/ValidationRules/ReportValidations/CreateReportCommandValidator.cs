using FluentValidation;
using Moderation.Application.Features.Report.Commands.CreateReport;

namespace Moderation.Application.ValidationRules.ReportValidations
{
    public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
    {
        public CreateReportCommandValidator()
        {
            RuleFor(x => x.Dto.ReporterId).NotEmpty().WithMessage("ReporterId zorunludur.");
            RuleFor(x => x.Dto.ReportedPlayerId).NotEmpty().WithMessage("ReportedPlayerId zorunludur.");
            RuleFor(x => x.Dto.Reason).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Dto.Description).NotEmpty().MaximumLength(1000);
        }
    }
}
