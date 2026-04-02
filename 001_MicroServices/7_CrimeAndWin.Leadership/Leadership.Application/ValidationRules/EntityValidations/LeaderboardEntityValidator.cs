using Leadership.Domain.Entities;
using Leadership.Domain.VOs;
using FluentValidation;
using Shared.Infrastructure.Validation;

namespace Leadership.Application.ValidationRules.EntityValidations;

public class LeaderboardEntityValidator : BaseEntityValidator<Leaderboard>
{
    public LeaderboardEntityValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x).Must(l => !l.IsSeasonal || (l.GameWorldId.HasValue && l.SeasonId.HasValue))
            .WithMessage("Sezonluk liderlik tabloları için GameWorld ve Season ID gereklidir.");
    }
}

public class LeaderboardEntryEntityValidator : BaseEntityValidator<LeaderboardEntry>
{
    public LeaderboardEntryEntityValidator()
    {
        RuleFor(x => x.LeaderboardId).NotEmpty();
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.Rank).NotNull().SetValidator(new RankValidator());
    }
}

public class RankValidator : AbstractValidator<Rank>
{
    public RankValidator()
    {
        RuleFor(x => x.RankPoints).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Position).Must(p => !p.HasValue || p > 0);
    }
}
