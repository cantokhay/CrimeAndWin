using Action.Domain.Entities;
using Action.Domain.VOs;
using Action.Domain.Enums;
using FluentValidation;
using Shared.Infrastructure.Validation;

namespace Action.Application.ValidationRules.EntityValidations;

public class ActionDefinitionEntityValidator : BaseEntityValidator<ActionDefinition>
{
    public ActionDefinitionEntityValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(32)
            .MatchesRegex("^[A-Z0-9_]+$", "Aksiyon Kodu");

        RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);

        RuleFor(x => x.Requirements).NotNull().SetValidator(new ActionRequirementsValidator());
        RuleFor(x => x.Rewards).NotNull().SetValidator(new ActionRewardsValidator());
    }
}

public class ActionRequirementsValidator : AbstractValidator<ActionRequirements>
{
    public ActionRequirementsValidator()
    {
        RuleFor(x => x.MinPower).GreaterThanOrEqualTo(0);
        RuleFor(x => x.EnergyCost).Positive(); // Shared: GreaterThan(0)
        RuleFor(x => x.DifficultyLevel).InRange(1, 10);
    }
}

public class ActionRewardsValidator : AbstractValidator<ActionRewards>
{
    public ActionRewardsValidator()
    {
        RuleFor(x => x.PowerGain).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MoneyGain).PositiveCurrency();
        // x.ItemDrop boolean, validasyona gerek yok
    }
}

public class PlayerActionAttemptEntityValidator : BaseEntityValidator<PlayerActionAttempt>
{
    public PlayerActionAttemptEntityValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.ActionId).NotEmpty();
        RuleFor(x => x.AttemptedAtUtc).IsPast();
        RuleFor(x => x.EnergySpent).Positive();
        RuleFor(x => x.Outcome).IsInEnum(); // Enum validation
    }
}

public class PlayerEnergyStateEntityValidator : BaseEntityValidator<PlayerEnergyState>
{
    public PlayerEnergyStateEntityValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.CurrentEnergy).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MaxEnergy).Positive();
        RuleFor(x => x.CurrentEnergy).LessThanOrEqualTo(x => x.MaxEnergy).WithMessage("Mevcut enerji maksimumu aşamaz.");
        RuleFor(x => x.LastUpdatedUtc).IsPast();
    }
}

public class GameSettingsEntityValidator : BaseEntityValidator<GameSettings>
{
    public GameSettingsEntityValidator()
    {
        RuleFor(x => x.Key).NotEmpty().MaximumLength(50).MatchesRegex("^[A-Za-z.]+$", "Ayar Anahtarı");
        RuleFor(x => x.Value).NotEmpty().MaximumLength(1024); // Support longer configs
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
