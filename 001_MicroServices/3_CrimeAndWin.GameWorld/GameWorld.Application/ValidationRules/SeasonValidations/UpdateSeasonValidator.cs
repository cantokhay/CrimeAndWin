using FluentValidation;
using GameWorld.Application.Features.Season.Commands.UpdateSeason;

namespace GameWorld.Application.ValidationRules.SeasonValidations
{
    public sealed class UpdateSeasonValidator : AbstractValidator<UpdateSeasonCommand>
    {
        public UpdateSeasonValidator()
        {
            RuleFor(x => x.SeasonId)
                .NotEmpty().WithMessage("SeasonId boþ olamaz.");

            RuleFor(x => x.SeasonNumber)
                .GreaterThan(0).WithMessage("SeasonNumber 0'dan büyük olmalýdýr.");

            RuleFor(x => x.StartUtc)
                .NotEmpty().WithMessage("StartUtc boþ olamaz.");

            RuleFor(x => x.EndUtc)
                .NotEmpty().WithMessage("EndUtc boþ olamaz.")
                .GreaterThan(x => x.StartUtc)
                .WithMessage("EndUtc, StartUtc tarihinden sonra olmalýdýr.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive deðeri belirtilmelidir.");
        }
    }
}


