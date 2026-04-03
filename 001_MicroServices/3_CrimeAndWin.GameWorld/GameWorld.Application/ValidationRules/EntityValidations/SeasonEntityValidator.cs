using GameWorld.Domain.VOs;
using FluentValidation;
using Shared.Infrastructure.Validation;

namespace GameWorld.Application.ValidationRules.EntityValidations;

public class GameWorldEntityValidator : BaseEntityValidator<Domain.Entities.GameWorld>
{
    public GameWorldEntityValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        // Removed invalid RuleFor(x => x.Code) and RuleFor(x => x.Settings)
        RuleFor(x => x.Rule).NotNull().SetValidator(new GameRuleValidator());
    }
}

public class GameRuleValidator : AbstractValidator<GameRule>
{
    public GameRuleValidator()
    {
        RuleFor(x => x.MaxEnergy).GreaterThan(0);
        RuleFor(x => x.RegenRatePerHour).GreaterThan(0);
    }
}

public class SeasonEntityValidator : BaseEntityValidator<Domain.Entities.Season>
{
    public SeasonEntityValidator()
    {
        RuleFor(x => x.GameWorldId).NotEmpty();
        RuleFor(x => x.SeasonNumber).GreaterThan(0);
        RuleFor(x => x.DateRange).NotNull().SetValidator(new DateRangeValidator());
    }
}

public class DateRangeValidator : AbstractValidator<DateRange>
{
    public DateRangeValidator()
    {
        RuleFor(x => x.StartUtc).NotEmpty();
        RuleFor(x => x.EndUtc).NotEmpty().GreaterThan(x => x.StartUtc).WithMessage("Bitiş tarihi başlangıçtan sonra olmalıdır.");
    }
}

