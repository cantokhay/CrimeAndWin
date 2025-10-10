using FluentValidation;
using GameWorld.Application.Features.Season.Commands.UpdateSeason;

namespace GameWorld.Application.ValidationRules.SeasonValidations
{
    public sealed class UpdateSeasonValidator : AbstractValidator<UpdateSeasonCommand>
    {
        public UpdateSeasonValidator()
        {
            RuleFor(x => x.SeasonId)
                .NotEmpty().WithMessage("SeasonId boş olamaz.");

            RuleFor(x => x.SeasonNumber)
                .GreaterThan(0).WithMessage("SeasonNumber 0'dan büyük olmalıdır.");

            RuleFor(x => x.StartUtc)
                .NotEmpty().WithMessage("StartUtc boş olamaz.");

            RuleFor(x => x.EndUtc)
                .NotEmpty().WithMessage("EndUtc boş olamaz.")
                .GreaterThan(x => x.StartUtc)
                .WithMessage("EndUtc, StartUtc tarihinden sonra olmalıdır.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive değeri belirtilmelidir.");
        }
    }
}
