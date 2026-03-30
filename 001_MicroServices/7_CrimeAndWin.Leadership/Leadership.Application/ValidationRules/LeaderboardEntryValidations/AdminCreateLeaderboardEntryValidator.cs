using FluentValidation;
using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;

namespace Leadership.Application.ValidationRules.LeaderboardEntryValidations
{
    public sealed class AdminCreateLeaderboardEntryValidator : AbstractValidator<AdminCreateLeaderboardEntryDTO>
    {
        public AdminCreateLeaderboardEntryValidator()
        {
            RuleFor(x => x.LeaderboardId).NotEmpty();
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.RankPoints).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Position).GreaterThan(0);
        }
    }

}
