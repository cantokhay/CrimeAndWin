using GameWorld.Domain.Entities;
using GameWorld.Domain.VOs;
using FluentValidation;
using Shared.Infrastructure.Validation;

namespace GameWorld.Application.ValidationRules.EntityValidations;

public class GameWorldEntityValidator : BaseEntityValidator<GameWorld.Domain.Entities.GameWorld>
{
    public GameWorldEntityValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Code).NotEmpty().MaximumLength(20).MatchesRegex("^[A-Z0-9]+$", "Dünya Kodu");
        RuleFor(x => x.Settings).NotNull().SetValidator(new GameRuleValidator());
    }
}

public class GameRuleValidator : AbstractValidator<GameRule>
{
    public GameRuleValidator()
    {
        RuleFor(x => x.MaxEnergy).Positive();
        RuleFor(x => x.RegenRatePerHour).Positive();
    }
}

public class SeasonEntityValidator : BaseEntityValidator<Season>
{
    public SeasonEntityValidator()
    {
        RuleFor(x => x.GameWorldId).NotEmpty();
        RuleFor(x => x.SeasonNumber).Positive();
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
