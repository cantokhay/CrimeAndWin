using FluentValidation;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;

namespace Leadership.Application.ValidationRules.LeaderboardEntryValidations
{
    public class CreateLeaderboardEntryDTOValidator : AbstractValidator<CreateLeaderboardEntryDTO>
    {
        public CreateLeaderboardEntryDTOValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.RankPoints).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Position).GreaterThanOrEqualTo(0);
            RuleFor(x => x.IsActive).NotNull();
        }
    }
}