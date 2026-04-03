using PlayerProfile.Domain.Entities;
using PlayerProfile.Domain.VOs;
using FluentValidation;
using Shared.Infrastructure.Validation;

namespace PlayerProfile.Application.ValidationRules.EntityValidations;

public class PlayerEntityValidator : BaseEntityValidator<Player>
{
    public PlayerEntityValidator()
    {
        RuleFor(x => x.AppUserId).NotEmpty();
        RuleFor(x => x.DisplayName).MaximumLength(32);
        RuleFor(x => x.AvatarKey).MaximumLength(100);
        RuleFor(x => x.Stats).NotNull().SetValidator(new PlayerStatsValidator());
        RuleFor(x => x.Energy).NotNull().SetValidator(new PlayerEnergyValidator());
        RuleFor(x => x.Rank).NotNull().SetValidator(new PlayerRankValidator());
        RuleFor(x => x.LastEnergyCalcUtc).IsPast();
    }
}

public class PlayerStatsValidator : AbstractValidator<Stats>
{
    public PlayerStatsValidator()
    {
        RuleFor(x => x.Power).Positive();
        RuleFor(x => x.Defense).Positive();
        RuleFor(x => x.Agility).Positive();
        RuleFor(x => x.Luck).Positive();
    }
}

public class PlayerEnergyValidator : AbstractValidator<Energy>
{
    public PlayerEnergyValidator()
    {
        RuleFor(x => x.Current).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Max).Positive();
        RuleFor(x => x.RegenPerMinute).InRange(1, 100);
        RuleFor(x => x.Current).LessThanOrEqualTo(x => x.Max).WithMessage("Mevcut enerji maksimumu aşamaz.");
    }
}

public class PlayerRankValidator : AbstractValidator<Rank>
{
    public PlayerRankValidator()
    {
        RuleFor(x => x.RankPoints).GreaterThanOrEqualTo(0);
        RuleFor(x => (int?)x.Position).Must(p => !p.HasValue || p > 0).WithMessage("Sıralama 1'den küçük olamaz.");
    }
}

